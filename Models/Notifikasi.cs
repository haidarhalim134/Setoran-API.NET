using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models;

public class Notifikasi
{
    [Key]
    public int IdNotifikasi { get; set; }
    public string IdPengguna { get; set; }
    public string? Judul { get; set; }
    public string? Deskripsi { get; set; }

    [DefaultValue(TargetNavigasi.None)]
    public TargetNavigasi Navigasi { get; set; } = TargetNavigasi.None;
    public string? DataNavigasi { get; set; }

    [DefaultValue(false)]
    public bool IsRead { get; set; } = false;
    public Pengguna? Pengguna { get; set; }

    public static Notifikasi CreateNotification(string idPengguna, string judul, string deskripsi)
    {
        return new Notifikasi {
            IdPengguna=idPengguna,
            Judul=judul,
            Deskripsi=deskripsi
        };
    }

    /// <summary>
    /// saat pengguna klik notifikasi, arahkan pengguna ke page transaksi tertentu
    /// </summary>
    public Notifikasi ToTransaksi(int idTransaksi)
    {
        Navigasi = TargetNavigasi.Transaksi;
        DataNavigasi = idTransaksi.ToString();

        return this;
    }

    /// <summary>
    /// saat pengguna klik notifikasi, arahkan pengguna ke page edit profile
    /// </summary>
    public Notifikasi ToEditProfile()
    {
        Navigasi = TargetNavigasi.EditProfile;

        return this;
    }

    public void Send(Database db)
    {
        db.Notifikasi.Add(this);

        // var devices = db.DeviceToken.Where(itm => itm.Pengguna == Pengguna).ToList();
        // foreach (var device in devices)
        // {
        //     // TODO: kirim notifikasi lewat firebase ke setiap device user ini

        // }
    }
}

public enum TargetNavigasi
{
    None,
    Transaksi,
    EditProfile
}