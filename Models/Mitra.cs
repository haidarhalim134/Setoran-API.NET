using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models
{
    public class Mitra
    {
        [Key]
        public int IdMitra { get; set; }
        // public int IdPengguna { get; set; }
        public string Status { get; set; }

        [ForeignKey("Id")]
        public Pengguna Pengguna { get; set; }
    }
}