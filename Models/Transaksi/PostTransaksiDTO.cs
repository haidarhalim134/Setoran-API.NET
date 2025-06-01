using System.ComponentModel.DataAnnotations;

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
    public int? idVoucher { get; set; }
    public int? idDiscount { get; set; }
}