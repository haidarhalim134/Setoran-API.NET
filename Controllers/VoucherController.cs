using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setoran_API.NET.Models;

namespace Setoran_API.NET.Controllers;

[ApiController]
[Route("[controller]")]
public class VoucherController : GenericControllerEXtension<Voucher>
{    

    // user endpoint
    [Authorize]
    [HttpGet("getActive")]
    public List<Voucher> GetActive(Database db)
    {
        // assume requester sudah ter registrasi sebagai pelanggan
        return Voucher.GetActive(db, HttpContext.GetCurrentPelanggan(db)).ToList();
    }

    [Authorize]
    [HttpGet("checkVoucher/{code}")]
    public ActionResult<CheckVoucherDTO> CheckVoucher(Database db, [FromRoute] string code)
    {
        // assume requester sudah ter registrasi sebagai pelanggan
        var voucher = Voucher.GetActive(db, HttpContext.GetCurrentPelanggan(db)).FirstOrDefault(v => v.KodeVoucher == code);
        if (voucher == null)
            return new CheckVoucherDTO {Valid=false};

        return new CheckVoucherDTO {Valid=true, Voucher=voucher};
    }

    // admin endpoint
    // [Authorize]
    [HttpPost()]
    public ActionResult<Voucher> Store(Database db, [FromBody] PostVoucherDTO voucher)
    {   
        var existing = db.Voucher.Where(v => v.KodeVoucher == voucher.KodeVoucher).FirstOrDefault();
        if (existing is not null)
            return BadRequest(new {message = "Kode voucher sudah digunakan"});

        db.Add(voucher.ToVoucher());
        db.SaveChanges();

        return Ok();
    }

    [HttpPut()]
    public ActionResult Update(Database db, [FromBody] Voucher voucher)
    {
        var voucherFound = db.Voucher.FirstOrDefault(v => v.IdVoucher == voucher.IdVoucher);
        if (voucherFound is null)
            return NotFound(new {message = "Voucher tidak ditemukan"});

        db.Entry(voucherFound).CurrentValues.SetValues(voucher);
        db.SaveChanges();

        return Ok();
    }

    [HttpGet("filtered")]
    public List<Voucher> GetFiltered(Database db, [FromQuery] string? search, [FromQuery] StatusVoucher? status, [FromQuery] DateTime? start, [FromQuery] DateTime? end)
    {
        var query = db.Voucher.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            // TODO: extend biar bisa search non string juga, e.g. date
            var properties = typeof(Voucher).GetProperties()
                .Where(p => p.PropertyType == typeof(string)).ToList(); 

            var whereClauses = new StringBuilder();
            var parameters = new List<object>();

            for (int i = 0; i < properties.Count; i++)
            {
                var column = properties[i].Name;
                if (i > 0) whereClauses.Append(" OR ");
                whereClauses.Append($"[{column}] LIKE @p{i}");
                parameters.Add($"%{search}%");
            }

            var interpolatedSql = FormattableStringFactory.Create($"select * from Voucher WHERE {whereClauses}", parameters.ToArray());

            // TODO: 
            query = db.Voucher.FromSqlInterpolated(interpolatedSql);
        }

        if (status != null)
            query = query.Where(v => v.StatusVoucher == status);

        // TODO: kurang jelas ngapain
        if (start.HasValue)
            query = query.Where(v => v.TanggalMulai <= start);

        if (end.HasValue)
            query = query.Where(v => v.TanggalAkhir >= end);

        var vouchers = query.ToList();

        return vouchers;
    }

    [HttpGet("getByCode/{code}")]
    public ActionResult<Voucher> GetByCode(Database db, [FromRoute] string code)
    {
        var voucher = db.Voucher.FirstOrDefault(v => v.KodeVoucher == code);
        if (voucher == null)
            return NotFound(new { message = "Voucher tidak ditemukan" });

        return Ok(voucher);
    }
}

