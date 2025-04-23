using System.ComponentModel.DataAnnotations;

namespace Setoran_API.NET.Models
{
    public class Voucher
    {
        [Key]
        public int Id { get; set; }
        public string Kode { get; set; }
        public decimal Potongan { get; set; }
        public string Tipe { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalAkhir { get; set; }
    }
}