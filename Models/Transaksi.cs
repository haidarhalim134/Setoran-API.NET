using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models
{
    public class Transaksi
    {
        [Key]
        public int IdTransaksi { get; set; }
        public int IdMotor { get; set; }
        public int IdPelanggan { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalSelesai { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalHarga { get; set; }
        public string Status { get; set; }

        [ForeignKey("IdMotor")]
        public Motor Motor { get; set; }

        [ForeignKey("IdPelanggan")]
        public Pelanggan Pelanggan { get; set; }

        public static void Seed(DbContext dbContext)
        {
            var faker = new Faker("id_ID");
            var motors = dbContext.Set<Motor>().ToList();
            var pelanggans = dbContext.Set<Pelanggan>().ToList();
            var statusOptions = new[] { "dibuat", "berlangsung", "batal", "selesai" };

            // buat populasi graph di dashboard website
            var dateRanges = new List<(string label, DateTime start, DateTime end)>
            {
                ("3_months", DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddMonths(-1)),
                ("30_days", DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(-7)),
                ("7_days", DateTime.UtcNow.AddDays(-7), DateTime.UtcNow),
            };

            foreach (var (label, startDate, endDate) in dateRanges)
            {
                for (int i = 0; i < 50; i++)
                {
                    var tanggalMulai = faker.Date.Between(startDate, endDate);
                    var durasi = faker.Random.Int(1, 5);
                    var tanggalSelesai = tanggalMulai.AddDays(durasi);
                    var hargaPerHari = faker.Random.Decimal(50000, 150000);
                    var totalHarga = hargaPerHari * durasi;

                    var transaksi = new Transaksi
                    {
                        IdMotor = faker.PickRandom(motors).IdMotor,
                        IdPelanggan = faker.PickRandom(pelanggans).IdPelanggan,
                        TanggalMulai = tanggalMulai,
                        TanggalSelesai = tanggalSelesai,
                        TotalHarga = totalHarga,
                        Status = faker.Random.Bool() ? "selesai" : faker.PickRandom(statusOptions)// ensure these are for graph
                    };

                    dbContext.Set<Transaksi>().Add(transaksi);
                }
            }

            dbContext.SaveChanges();
        }
    }
}