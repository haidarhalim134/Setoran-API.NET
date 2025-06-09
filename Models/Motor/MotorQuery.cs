using Setoran_API.NET.Models;

public class MotorQuery
{
    public bool WithImage { get; set; } = false;
    public bool WithDiskon { get; set; } = false;
    public bool WithUlasan { get; set; } = false;
    public String? IdMitra { get; set; }
    public StatusMotor? Status { get; set; }
    public string? Model { get; set; }
    public TransmisiMotor? Transmisi { get; set; }

}