using System.ComponentModel.DataAnnotations;

namespace Setoran_API.NET.Models;

public record class MotorForm
{
    [Required]
    [MaxLength(9)]
    public string PlatNomor { get; set; }

    [Required]
    public int IdMitra { get; set; }

    [Required]

    public string NomorSTNK { get; set; }

    [Required]
    public string NomorBPKB { get; set; }

    [Required]
    public string Model { get; set; }

    [Required]
    public string Brand { get; set; }

    [Required]
    public string Tipe { get; set; }

    [Required]
    public int Tahun { get; set; }

    [Required]
    public string Transmisi { get; set; }

    [Required]
    public string StatusMotor { get; set; }

    [Required]
    public decimal HargaHarian { get; set; }
}
