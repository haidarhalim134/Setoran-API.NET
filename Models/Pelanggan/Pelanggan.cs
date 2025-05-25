using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models;

public class Pelanggan
{
    [Key]
    public int IdPelanggan { get; set; }
    public string IdPengguna { get; set; }
    public Pengguna Pengguna { get; set; }
    public string? NomorSIM { get; set; }

    /// <summary>
    /// list voucher yang pernah digunakan pelanggan ini
    /// </summary>
    public List<Voucher> UsedVouchers { get; set; }

    public static void Seed(DbContext dbContext)
    {
        var random = new Random();
        foreach (var pengguna in dbContext.Set<Pengguna>())
        {
            // seed ke pelanggan
            var pelanggan = new Pelanggan
            {
                IdPengguna = pengguna.Id,
                NomorSIM = string.Concat(Enumerable.Range(0, 12).Select(_ => random.Next(0, 10).ToString()))
            };
            dbContext.Set<Pelanggan>().Add(pelanggan);
        }
        dbContext.SaveChanges();
    }
}