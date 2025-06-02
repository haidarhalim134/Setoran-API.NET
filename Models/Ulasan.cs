using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}