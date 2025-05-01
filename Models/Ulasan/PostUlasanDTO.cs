using System.ComponentModel.DataAnnotations;

public record class PostUlasanDTO
{
    [Required]
    public int IdMotor { get; set; }
    [Required]
    public int IdPelanggan { get; set; }
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
    [Required]
    [MaxLength(500)]
    public string Komentar { get; set; }
}