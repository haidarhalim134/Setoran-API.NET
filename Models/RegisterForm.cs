using System.ComponentModel.DataAnnotations;

namespace Setoran_API.NET.Controllers;

public record RegisterForm
{ 
    [Required]
    [MaxLength(255)]
    public string nama { get; set; } 

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string email { get; set; } 

    [Required]
    [MinLength(8)]
    public string password { get; set; } 
}