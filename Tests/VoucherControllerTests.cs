
using dotenv.net;
using Moq;
using Setoran_API.NET.Controllers;

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

        var result = _controller.GetFiltered(_db, status: Setoran_API.NET.Models.StatusVoucher.Aktif);

        foreach (var voucher in result)
        {
            Assert.Equal(voucher.StatusVoucher, Setoran_API.NET.Models.StatusVoucher.Aktif);
        }
    }
}