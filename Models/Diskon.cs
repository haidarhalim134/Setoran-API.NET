using System.ComponentModel.DataAnnotations;

namespace Setoran_API.NET.Models
{
    public class Diskon
    {
        [Key]
        public int Id { get; set; }
        public string Nama { get; set; }
        public string StatusPromo { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalAkhir { get; set; }
    }
}