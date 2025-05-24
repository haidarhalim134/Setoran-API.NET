using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
    public Dictionary<string, string>? DataNavigasi { get; set; }

    [DefaultValue(false)]
    public bool IsRead { get; set; } = false;
    public Pengguna? Pengguna { get; set; }

    public DateTime WaktuNotifikasi { get; set; } = DateTime.Now;

    public static Notifikasi CreateNotification(string idPengguna, string judul, string deskripsi)
    {
        return new Notifikasi
        {
            IdPengguna = idPengguna,
            Judul = judul,
            Deskripsi = deskripsi
        };
    }

    /// <summary>
    /// saat pengguna klik notifikasi, arahkan pengguna ke page transaksi tertentu
    /// </summary>
    public Notifikasi ToTransaksi(int idTransaksi)
    {
        Navigasi = TargetNavigasi.Transaksi;
        DataNavigasi = new Dictionary<string, string> { {"id_transaksi", idTransaksi.ToString()}};

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

    public void Send(DbContext db, bool sendFcm=true)
    {
        db.Set<Notifikasi>().Add(this);
        WaktuNotifikasi = DateTime.Now;

        if (sendFcm)
        {
            // var devices = db.Set<DeviceToken>().Where(itm => itm.Pengguna == Pengguna).ToList();
            // foreach (var device in devices)
            // {
            //     // TODO: kirim notifikasi lewat firebase ke setiap device user ini

            // }
        }

    }

    public static void Seed(DbContext dbContext)
    {

        var users = dbContext.Set<Pengguna>().ToList();
        foreach (var user in users)
        {
            CreateNotification(user.Id, "Selamat datang di aplikasi Setoran", "Silahkan selesaikan proses registrasi dengan melengkapi data-data anda di halaman edit profile")
                .ToEditProfile()
                .Send(dbContext, false);
        }

        dbContext.SaveChanges();
    }
}

public enum TargetNavigasi
{
    None,
    Transaksi,
    EditProfile
}