using System.ComponentModel.DataAnnotations;
using Mapster;

namespace Setoran_API.NET.Models;

public class PostPelangganDTO
{
    [Required]
    public int IdPelanggan { get; set; }
    public string? NomorSIM { get; set; }

    public Pelanggan ToPelanggan()
    {
        return this.Adapt<Pelanggan>();
    }
}