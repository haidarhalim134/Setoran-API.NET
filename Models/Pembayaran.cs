using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models
{

    public class Pembayaran
    {
        [Key]
        public int IdPembayaran { get; set; }
        public int IdTransaksi { get; set; }
        public MetodePembayaran MetodePembayaran { get; set; }
        public StatusPembayaran StatusPembayaran { get; set; }
        public DateTime? TanggalPembayaran { get; set; }

        [ForeignKey("IdTransaksi")]
        public Transaksi Transaksi { get; set; }
    }

    public enum StatusPembayaran
    {
        BelumLunas,
        MenungguKonfirmasi,
        Lunas,
        Gagal
    }

    public enum MetodePembayaran
    {
        TransferBank,
        KartuKredit,
        DompetDigital,
        Tunai
    }
}