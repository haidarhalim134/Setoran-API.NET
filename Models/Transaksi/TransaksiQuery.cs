using Setoran_API.NET.Models;

public class TransaksiQuery
{
    public string? IdMotor { get; set; }
    public string? IdPelanggan { get; set; }
    public string? IdMitra { get; set; }
    public StatusTransaksi? Status { get; set; }
}