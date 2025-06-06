using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models
{
    public class Motor
    {
        [Key]
        public int IdMotor { get; set; }
        public string PlatNomor { get; set; }
        public int IdMitra { get; set; }
        public string NomorSTNK { get; set; }
        public string NomorBPKB { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Tipe { get; set; }
        public int Tahun { get; set; }
        public string Transmisi { get; set; }
        public string StatusMotor { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal HargaHarian { get; set; }
        public List<Diskon> Diskon { get; set; }
        public List<Ulasan> Ulasan { get; set; }
        public int? IdMotorImage { get; set; }

        [ForeignKey("IdMitra")]
        public Mitra Mitra { get; set; }

        [ForeignKey("IdMotorImage")]
        public MotorImage? MotorImage { get; set; }

        public static void Seed(DbContext dbContext)
        {
            var mitra = dbContext.Set<Mitra>().ToList()[0];

            dbContext.Set<Motor>().Add(new Motor
            {
                PlatNomor = "D1234QWE",
                IdMitra = mitra.IdMitra,
                NomorSTNK = "",
                NomorBPKB = "",
                Model = "Beat",
                Brand = "",
                Tipe = "",
                Tahun = 2022,
                Transmisi = "Matic",
                StatusMotor = "Tersedia",
                HargaHarian = 70000,
            });

            dbContext.Set<Motor>().Add(new Motor
            {
                PlatNomor = "D2345ASD",
                IdMitra = mitra.IdMitra,
                NomorSTNK = "",
                NomorBPKB = "",
                Model = "Ninja",
                Brand = "",
                Tipe = "",
                Tahun = 2021,
                Transmisi = "Manual",
                StatusMotor = "Tersedia",
                HargaHarian = 120000,
            });

            dbContext.Set<Motor>().Add(new Motor
            {
                PlatNomor = "D3210RTY",
                IdMitra = mitra.IdMitra,
                NomorSTNK = "",
                NomorBPKB = "",
                Model = "Scoopy",
                Brand = "",
                Tipe = "",
                Tahun = 2022,
                Transmisi = "Matic",
                StatusMotor = "Tersedia",
                HargaHarian = 70000,
            });

            dbContext.Set<Motor>().Add(new Motor
            {
                PlatNomor = "D5678BNM",
                IdMitra = mitra.IdMitra,
                NomorSTNK = "",
                NomorBPKB = "",
                Model = "Beat",
                Brand = "",
                Tipe = "",
                Tahun = 2022,
                Transmisi = "Matic",
                StatusMotor = "Tersedia",
                HargaHarian = 70000,
            });

            dbContext.SaveChanges();
        }
    }
}