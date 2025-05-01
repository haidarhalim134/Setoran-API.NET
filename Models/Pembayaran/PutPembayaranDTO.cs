using System.ComponentModel.DataAnnotations;

public class PutPembayaranDTO
{
    public string MetodePembayaran { get; set; }
    public string StatusPembayaran { get; set; }
    public DateTime? TanggalPembayaran { get; set; }
}