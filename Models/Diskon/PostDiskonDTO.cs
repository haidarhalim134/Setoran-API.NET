
using Mapster;

namespace Setoran_API.NET.Models;

public class PostDiskonDTO
{
    public int? IdMotor { get; set; }
    public string? Nama { get; set; }
    public string? Deskripsi { get; set; }
    public decimal? JumlahDiskon { get; set; }
    public StatusDiskon? StatusDiskon { get; set; }
    public DateTime? TanggalMulai { get; set; }
    public DateTime? TanggalAkhir { get; set; }

    public Diskon ToDiskon()
    {
        return this.Adapt<Diskon>();
    }
}