using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models
{
    public class MotorImage
    {
        [Key]
        public int Id { get; set; }
        public int IdMotor { get; set; }
        public String Front { get; set; }
        public String Left { get; set; }
        public String Right { get; set; }
        public String Rear { get; set; }

        [ForeignKey("IdMotor")]
        public Motor Motor { get; set; }
    }
}