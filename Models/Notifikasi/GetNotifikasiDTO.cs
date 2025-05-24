using Mapster;

namespace Setoran_API.NET.Models;

// dto notifikasi tanpa navigational prop
public class GetNotifikasDTO
{
    public int IdNotifikasi { get; set; }
    public string IdPengguna { get; set; }
    public string? Judul { get; set; }
    public string? Deskripsi { get; set; }
    public TargetNavigasi Navigasi { get; set; }
    public Dictionary<string, string>? DataNavigasi { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime WaktuNotifikasi { get; set; }

    public static GetNotifikasDTO FromNotif(Notifikasi notifikasi)
    {
        return notifikasi.Adapt<GetNotifikasDTO>();
    }
}