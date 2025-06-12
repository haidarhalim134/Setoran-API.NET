using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models
{
    public class Ulasan
    {
        [Key]
        public int IdUlasan { get; set; }
        public int IdMotor { get; set; }
        public int IdPelanggan { get; set; }
        public int Rating { get; set; }
        public string Komentar { get; set; }
        public DateTime TanggalUlasan { get; set; }
        public Motor Motor { get; set; }

        [ForeignKey("IdPelanggan")]
        public Pelanggan Pelanggan { get; set; }

        public static void Seed(DbContext dbContext)
        {
            var faker = new Faker("id_ID");
            var random = new Random();
            var motors = dbContext.Set<Motor>().Include(m => m.Mitra).ToList();
            var pelanggans = dbContext.Set<Pelanggan>().ToList();

            foreach (var motor in motors)
            {
                for (int i = 0; i < random.Next(0, 4); i++)
                {
                    var ulasan = new Ulasan
                    {
                        IdMotor = motor.IdMotor,
                        IdPelanggan = faker.PickRandom(pelanggans.Where(p => p.IdPengguna != motor.Mitra.IdPengguna)).IdPelanggan,
                        Rating = random.Next(2, 6),
                        Komentar = "Ini adalah komentar ulasan",
                        TanggalUlasan = DateTime.Now
                    };

                    dbContext.Add(ulasan);
                }
            }

            dbContext.SaveChanges();
        }
    }
}