using System.ComponentModel.DataAnnotations;

namespace Setoran_API.NET.Models
{
    public class Voucher
    {
        [Key]
        public int IdVoucher { get; set; }
        public string NamaVoucher { get; set; }
        public StatusVoucher StatusVoucher { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalAkhir { get; set; }
        
        [Range(1, 100)]
        public int PersenVoucher { get; set; }
        public string KodeVoucher { get; set; }

        public List<Voucher> GetActive(Database db)
        {
            return db.Voucher
                .Where(v => v.StatusVoucher == StatusVoucher.Aktif)
                .Where(v => v.TanggalMulai <= DateTime.Now && DateTime.Now <= v.TanggalAkhir)
                .ToList();
        }
        
    }
    public enum StatusVoucher
    {
        Aktif,
        NonAktif
    }
}