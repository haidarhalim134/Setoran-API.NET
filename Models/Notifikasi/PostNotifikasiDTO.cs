using System.ComponentModel;
using Mapster;

namespace Setoran_API.NET.Models;

public class PostNotifikasDTO
{
    public string IdPengguna { get; set; }
    public string? Judul { get; set; }
    public string? Deskripsi { get; set; }

    [DefaultValue(TargetNavigasi.None)]
    public TargetNavigasi Navigasi { get; set; } = TargetNavigasi.None;
    public string? DataNavigasi { get; set; }

    [DefaultValue(false)]
    public bool IsRead { get; set; } = false;

    public Notifikasi ToNotif()
    {
        return this.Adapt<Notifikasi>();
    }
}