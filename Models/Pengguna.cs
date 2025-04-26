using Microsoft.AspNetCore.Identity;

namespace Setoran_API.NET.Models;

public class Pengguna: IdentityUser
{
    public Pelanggan? Pelanggan { get; set; }
    public List<Notifikasi> Notifikasis { get; set; }
    public string Nama { get; set; }
    public DateTime? TanggalLahir { get; set; }
    public string NomorTelepon { get; set; }
    public int? Umur { get; set; }
    public string NomorKTP { get; set; }
    public string Alamat { get; set; }
    public int? IdGambar { get; set; }
}

