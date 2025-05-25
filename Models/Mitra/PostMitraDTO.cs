using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models
{
    public class PostMitraDTO
    {
        [Key]
        public int IdMitra { get; set; }
        public StatusMitra Status { get; set; }
    }
}