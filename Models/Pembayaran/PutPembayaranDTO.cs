using System.ComponentModel.DataAnnotations;
using Setoran_API.NET.Models;

public class PutPembayaranDTO
{
    public MetodePembayaran MetodePembayaran { get; set; }
    public StatusPembayaran StatusPembayaran { get; set; }
    public DateTime? TanggalPembayaran { get; set; }
}