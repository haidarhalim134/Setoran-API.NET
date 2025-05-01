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
        public int PersenVoucher { get; set; }
        public string KodeVoucher { get; set; }

        /// <summary>
        /// list pelanggan yang pernah menggunakan voucher ini
        /// </summary>
        public List<Pelanggan> Pelanggans { get; set; }

        public static IQueryable<Voucher> GetActive(Database db, Pelanggan pelanggan)
        {
            return db.Voucher
                .Where(v => v.StatusVoucher == StatusVoucher.Aktif)
                .Where(v => v.TanggalMulai <= DateTime.Now && DateTime.Now <= v.TanggalAkhir)
                .Where(v => !db.VoucherUsed.Any(vu => vu.Voucher.IdVoucher == v.IdVoucher && vu.Pelanggan.IdPelanggan == pelanggan.IdPelanggan));
        }
        
        public static void UseVoucher(Database db, Voucher voucher, Pelanggan pelanggan)
        {
            var voucherUsed = new VoucherUsed {
                Voucher=voucher,
                Pelanggan=pelanggan
            };

            db.Add(voucherUsed);
            db.SaveChanges();
        }
    }
    public enum StatusVoucher
    {
        Aktif,
        NonAktif
    }
}