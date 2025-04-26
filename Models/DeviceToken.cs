using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models;

public class DeviceToken
{
    [Key]
    public int IdToken { get; set; }
    public string IdPengguna { get; set; }
    public Pengguna Pengguna { get; set; }
    public string Token { get; set; }
}