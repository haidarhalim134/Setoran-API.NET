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

    public int AmountPerPage { get; set; } = 10;
    public int Page { get; set; } = 1;
    public MotorSorting? Sorting { get; set; }
}

public enum MotorSorting
{
    None,
    MostPopular,
    BestDiscount,
    BestRating
}