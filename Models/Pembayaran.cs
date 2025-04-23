using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models
{
    
    public class Pembayaran
    {
        [Key]
        public int IdPembayaran { get; set; }
        public int IdTransaksi { get; set; }
        public string MetodePembayaran { get; set; }
        public string StatusPembayaran { get; set; }
        public DateTime TanggalPembayaran { get; set; }

        [ForeignKey("IdTransaksi")]
        public Transaksi Transaksi { get; set; }
    }
}