using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setoran_API.NET.Models;

public class VoucherUsed
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("IdVoucher")]
    public Voucher Voucher { get; set; }

    [ForeignKey("IdPelanggan")]
    public Pelanggan Pelanggan { get; set; }
}