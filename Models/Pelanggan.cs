using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models;

public class Pelanggan
{
    [Key]
    public int IdPelanggan { get; set; }
    public string IdPengguna { get; set; }
    public Pengguna Pengguna { get; set; }
    public string? NomorSIM { get; set; }
}