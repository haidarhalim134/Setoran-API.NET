using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }

    public enum StatusDiskon
    {
        Aktif,
        NonAktif
    }
}