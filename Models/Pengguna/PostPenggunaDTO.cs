using System.ComponentModel.DataAnnotations;
using Mapster;

namespace Setoran_API.NET.Models;

public class PostPenggunaDTO
{
    [Required]
    public string Id { get; set; }
    public string? Nama { get; set; }
    public DateTime? TanggalLahir { get; set; }
    public string? NomorTelepon { get; set; }
    public int? Umur { get; set; }
    public string? NomorKTP { get; set; }
    public string? Alamat { get; set; }
    public string? IdGambar { get; set; }

    public Pengguna ToPengguna()
    {
        return this.Adapt<Pengguna>();
    }
}