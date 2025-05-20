using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Setoran_API.NET.Controllers;

namespace Tests;

public class MotorControllerTest : TestBase
{
    private readonly MotorController _controller;

    public MotorControllerTest() : base()
    {
        _controller = new MotorController(_db);
    }

    [Fact]
    public async Task Get_All_Motor_Ok()
    {
        // Given
        MotorQuery query = new MotorQuery { };

        // When
        var result = await _controller.GetMotors(query);

        // Then
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task Get_Motor_By_Mitra_Ok()
    {
        // Given
        MotorQuery query = new MotorQuery
        {
            IdMitra = "1"
        };

        // When
        var result = await _controller.GetMotors(query);
        var listMotor = result.Value;

        // Then
        Assert.IsType<OkObjectResult>(result.Result);
        if (listMotor != null)
        {
            Assert.All(listMotor, motor => Assert.Equal(1, motor.IdMitra));

        }
    }

    [Fact]
    public async Task Get_Motor_By_Mitra_And_Status_OK()
    {
        // Given
        MotorQuery query = new MotorQuery
        {
            IdMitra = "1",
            Status = "Tersedia"

        };

        // When
        var result = await _controller.GetMotors(query);
        var listMotor = result.Value;


        // Then
        Assert.IsType<OkObjectResult>(result.Result);
        if (listMotor != null)
        {
            Assert.All(listMotor, motor =>
            {
                Assert.Equal(1, motor.IdMitra);
                Assert.Equal("Tersedia", motor.StatusMotor);
            });
        }
    }

    [Fact]
    public async Task Get_Motor_Using_All_Query()
    {
        // Given
        MotorQuery query = new MotorQuery
        {
            IdMitra = "1",
            Model = "Beat",
            Status = "Tersedia",
            Transmisi = "Matic"
        };

        // When
        var result = await _controller.GetMotors(query);
        var listMotor = result.Value;

        // Then
        Assert.IsType<OkObjectResult>(result.Result);
        if (listMotor != null)
        {
            Assert.All(listMotor, motor =>
            {
                Assert.Equal(1, motor.IdMitra);
                Assert.Equal("Beat", motor.Model);
                Assert.Equal("Tersedia", motor.StatusMotor);
                Assert.Equal("Matic", motor.Transmisi);
            });
        }
    }

    [Fact]
    public async Task Get_Motor_By_Transmisi()
    {
        // Given
        MotorQuery query = new MotorQuery
        {
            Transmisi = "Matic"
        };

        // When
        var result = await _controller.GetMotors(query);
        var listMotor = result.Value;

        // Then
        Assert.IsType<OkObjectResult>(result.Result);
        if (listMotor != null)
        {
            Assert.All(listMotor, motor => Assert.Equal("Matic", motor.Transmisi));
        }
    }

    [Fact]
    public async Task Get_Motor_By_Invalid_IdMitra()
    {
        // Given
        MotorQuery query = new MotorQuery
        {
            IdMitra = "-123"
        };

        // When
        var result = await _controller.GetMotors(query);
        var listMotor = result.Value;

        // Then
        Assert.IsType<OkObjectResult>(result.Result);
        if (listMotor != null)
        {
            Assert.All(listMotor, motor => Assert.NotEqual(int.Parse("123zbc"), motor.IdMitra));
        }
    }

    [Fact]
    public async Task Get_Motor_With_Some_Empty_String_Query()
    {
        // Given
        MotorQuery query = new MotorQuery
        {
            IdMitra = "",
            Model = "Beat",
            Status = "",
            Transmisi = "Matic"
        };

        // When
        var result = await _controller.GetMotors(query);
        var listMotor = result.Value;

        // Then
        Assert.IsType<OkObjectResult>(result.Result);
        if (listMotor != null)
        {
            Assert.All(listMotor, motor =>
            {
                Assert.NotEqual(int.Parse(""), motor.IdMitra);
                Assert.Equal("Beat", motor.Model);
                Assert.NotEqual("", motor.StatusMotor);
                Assert.Equal("Matic", motor.Transmisi);
            });
        }
    }
}
