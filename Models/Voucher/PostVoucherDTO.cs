using System.ComponentModel.DataAnnotations;
using Mapster;

namespace Setoran_API.NET.Models;

public class PostVoucherDTO
{
    [Required]
    public string NamaVoucher { get; set; }

    [Required]
    public StatusVoucher StatusVoucher { get; set; }

    [Required]
    public DateTime TanggalMulai { get; set; }

    [Required]
    public DateTime TanggalAkhir { get; set; }

    [Required]
    [Range(1, 100)]
    public int PersenVoucher { get; set; }
    [Required]
    public string KodeVoucher { get; set; }

    public Voucher ToVoucher()
    {
        return this.Adapt<Voucher>();
    }
}