
using Setoran_API.NET.Models;

namespace Setoran_API.NET;

// Mungkin pindah ke folder sendiri nanti

public static class Extensions
{
    public static Pengguna? GetCurrentPengguna(this HttpContext context, Database db)
    {
        return db.Pengguna.Where(itm => itm.UserName == context!.User!.Identity!.Name).FirstOrDefault();
    }
    public static Pelanggan? GetCurrentPelanggan(this HttpContext context, Database db)
    {
        var pengguna = context.GetCurrentPengguna(db);
        if (pengguna != null)
        {
            db.Entry(pengguna).Reference(itm => itm.Pelanggan).Load();
            return pengguna.Pelanggan;
        }

        return null;
    }
}