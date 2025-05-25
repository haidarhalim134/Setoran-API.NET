using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Setoran_API.NET.Models
{
    public class Mitra
    {
        [Key]
        public int IdMitra { get; set; }
        public StatusMitra Status { get; set; }

        public string IdPengguna { get; set; }
        public Pengguna Pengguna { get; set; }

        public static void Seed(DbContext dbContext)
        {
            var random = new Random();
            var statuses = Enum.GetValues(typeof(StatusMitra));
            
            foreach (var pengguna in dbContext.Set<Pengguna>())
            {
                // seed ke pelanggan
                var pelanggan = new Mitra
                {
                    IdPengguna = pengguna.Id,
                    Status = (StatusMitra)statuses.GetValue(random.Next(statuses.Length))
                };
                dbContext.Set<Mitra>().Add(pelanggan);
            }
            dbContext.SaveChanges();
        }
    }

    public enum StatusMitra
    {      
        Aktif,
        NonAktif
    }
}