
using dotenv.net;
using Moq;
using Setoran_API.NET.Controllers;
using Setoran_API.NET.Models;

public class VoucherControllerTests : TestBase
{

    private readonly VoucherController _controller;

    public VoucherControllerTests() : base()
    {
        _controller = new VoucherController();
    }

    [Fact]
    public void Voucher_filtered_activeFilter()
    {
        // input: StatusVoucher.Aktif
        var result = _controller.GetFiltered(_db, status: Setoran_API.NET.Models.StatusVoucher.Aktif);

        foreach (var voucher in result)
        {
            Assert.Equal(voucher.StatusVoucher, Setoran_API.NET.Models.StatusVoucher.Aktif);
        }
        Console.WriteLine("TC-01 Output:");
        Print(result);
    }

    [Fact]
    public void Voucher_filtered_endDataFilter()
    {
        // input: end = 20 april 2025
        var inputDate = DateTime.SpecifyKind(new DateTime(year: 2025, month: 4, day: 20), DateTimeKind.Utc);

        var result = _controller.GetFiltered(_db, end: inputDate);

        foreach (var voucher in result)
        {
            Assert.True(voucher.TanggalAkhir >= inputDate);
        }
        Console.WriteLine("TC-02 Output:");
        Print(result);
    }

    public void Print(List<Voucher> vouchers)
    {
        if (vouchers.Count == 0)
            Console.WriteLine("None");

        foreach (var voucher in vouchers)
        {
             
            Console.WriteLine($@"IdVoucher         = {voucher.IdVoucher}
            KodeVoucher       = ""{voucher.KodeVoucher}""
            NamaVoucher       = ""{voucher.NamaVoucher}""
            PersenVoucher     = {voucher.PersenVoucher}
            StatusVoucher     = {voucher.StatusVoucher}
            TanggalMulai      = {voucher.TanggalMulai}
            TanggalAkhir      = {voucher.TanggalAkhir}");
            
        }
    }
}