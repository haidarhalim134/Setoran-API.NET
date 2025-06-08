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
public class VoucherController : GenericControllerExtension<Voucher>
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
            
        var newVoucher = voucher.ToVoucher();
        db.Add(newVoucher);
        db.SaveChanges();

        return Ok(newVoucher);
    }

    [HttpPut("{idVoucher}")]
    public ActionResult Update(Database db, [FromRoute] int idVoucher, [FromBody] PostVoucherDTO voucher)
    {
        var voucherFound = db.Voucher.FirstOrDefault(v => v.IdVoucher == idVoucher);
        if (voucherFound is null)
            return NotFound(new {message = "Voucher tidak ditemukan"});

        db.Entry(voucherFound).CurrentValues.SetValues(voucher);
        db.SaveChanges();

        return Ok();
    }

    [HttpGet("filtered")]
    public List<Voucher> GetFiltered(Database db, [FromQuery] string? search=null, [FromQuery] StatusVoucher? status=null, [FromQuery] DateTime? start=null, [FromQuery] DateTime? end=null)
    {
        var query = db.Voucher.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            var properties = typeof(Voucher).GetProperties().ToList();

            var whereClauses = new StringBuilder();
            var parameters = new List<object>();
            int paramIndex = 0;

            foreach (var prop in properties)
            {
                var column = prop.Name;

                if (prop.PropertyType == typeof(string))
                {
                    if (whereClauses.Length > 0) whereClauses.Append(" OR ");
                    whereClauses.Append($"LOWER(\"{column}\") LIKE LOWER(@p{paramIndex})");
                    parameters.Add($"%{search}%");
                    paramIndex++;
                }
                else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                {
                    if (paramIndex > 0) whereClauses.Append(" OR ");
                    whereClauses.Append($"LOWER(TO_CHAR(\"{prop.Name}\", 'FMDD FMMonth YYYY')) LIKE LOWER(@p{paramIndex})");
                    parameters.Add($"%{search}%");
                    paramIndex++;
                }
            }

            if (whereClauses.Length > 0)
            {
                var interpolatedSql = FormattableStringFactory.Create(
                    $"SELECT * FROM \"Voucher\" WHERE {whereClauses}", parameters.ToArray());

                query = db.Voucher.FromSqlInterpolated(interpolatedSql);
            }
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

