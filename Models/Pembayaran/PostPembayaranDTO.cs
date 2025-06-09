using System.ComponentModel.DataAnnotations;
using Setoran_API.NET.Models;

public class PostPembayaranDTO
{
    [Required]
    public int IdTransaksi { get; set; }
    [Required]
    public MetodePembayaran MetodePembayaran { get; set; }
}