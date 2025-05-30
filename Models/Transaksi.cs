using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}