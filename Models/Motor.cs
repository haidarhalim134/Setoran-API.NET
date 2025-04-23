using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public decimal HargaHarian { get; set; }
        public int? DiskonPercentage { get; set; }
        public int? DiskonAmount { get; set; }

        [ForeignKey("IdMitra")]
        public Mitra Mitra { get; set; }
    }
}