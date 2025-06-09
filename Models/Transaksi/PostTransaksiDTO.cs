using System.ComponentModel.DataAnnotations;
using Setoran_API.NET.Models;

public record class PostTransaksiDTO
{
    [Required]
    public int IdMotor { get; set; }
    [Required]
    public int IdPelanggan { get; set; }
    [Required]
    public DateTime TanggalMulai { get; set; }
    [Required]
    public DateTime TanggalSelesai { get; set; }
    [Required]
    public MetodePembayaran MetodePembayaran { get; set; }
    public int? idVoucher { get; set; }
    public int? idDiscount { get; set; }
}