
namespace Setoran_API.NET.Models;

public class DashboardDataDTO
{
    public int AvailableMotorCount { get; set; }
    public int FiledMotorCount { get; set; }
    public int TransaksiOngoing { get; set; }
    public decimal TotalIncome { get; set; }
    public List<Transaksi> ChartTransaksi { get; set; } = [];
}