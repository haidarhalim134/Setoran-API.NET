using System.ComponentModel.DataAnnotations;

public class PostPembayaranDTO
{
    [Required]
    public int IdTransaksi { get; set; }
    [Required]
    public string MetodePembayaran { get; set; }
}