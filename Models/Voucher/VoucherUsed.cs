using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models;

public class VoucherUsed
{
    public Voucher Voucher { get; set; }
    public Pelanggan Pelanggan { get; set; }
}