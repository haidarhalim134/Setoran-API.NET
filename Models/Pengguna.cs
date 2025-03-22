using Microsoft.AspNetCore.Identity;

namespace Setoran_API.NET.Models;

public class Pengguna: IdentityUser<int>
{
    public Pelanggan? Pelanggan { get; set; }
}

