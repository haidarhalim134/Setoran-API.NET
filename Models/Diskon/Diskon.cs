using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models
{
    public class Diskon
    {
        [Key]
        public int IdDiskon { get; set; }
        public int IdMotor { get; set; }
        public Motor Motor { get; set; }
        public string Nama { get; set; }
        public string? Deskripsi { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal JumlahDiskon { get; set; }
        public StatusDiskon StatusDiskon { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalAkhir { get; set; }

        public static void Seed(DbContext dbContext)
        {
            var faker = new Faker("id_ID");
            foreach (var motor in dbContext.Set<Motor>())
            {
                // if (!faker.Random.Bool())
                //     continue;
                    
                var tanggalMulai = faker.Date.PastOffset(1).UtcDateTime.Date;
                var daysToAdd = faker.Random.Int(1, 10);
                var tanggalAkhir = tanggalMulai.AddDays(daysToAdd).ToUniversalTime();

                var diskon = new Diskon
                {
                    Nama = string.Join(" ", faker.Lorem.Words(2)),
                    IdMotor = motor.IdMotor,
                    StatusDiskon = faker.PickRandom(new[] { StatusDiskon.Aktif, StatusDiskon.NonAktif }),
                    TanggalMulai = tanggalMulai,
                    TanggalAkhir = tanggalAkhir,
                    JumlahDiskon = faker.Random.Decimal(1000, motor.HargaHarian - 1000),
                };

                dbContext.Set<Diskon>().Add(diskon);
            }

            dbContext.SaveChanges();
        }
    }

    public enum StatusDiskon
    {
        Aktif,
        NonAktif
    }
}