using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models
{
    public class Mitra
    {
        [Key]
        public int IdMitra { get; set; }
        public string Status { get; set; }

        public string IdPengguna { get; set; }
        public Pengguna Pengguna { get; set; }
    }
}