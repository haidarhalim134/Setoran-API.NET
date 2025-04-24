namespace Setoran_API.NET.Models;

public class CheckVoucherDTO
{
    public bool Valid { get; set; }
    public Voucher? Voucher { get; set; }
}