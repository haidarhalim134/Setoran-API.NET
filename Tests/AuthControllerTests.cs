// using Xunit;
// using Moq;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using System.Collections.Generic;
// using System.Security.Claims;
// using Setoran_API.NET.Models;
// using Setoran_API.NET.Controllers;

// public class AuthControllerTests : TestBase
// {
//     private readonly AuthController _controller;
//     private Mock<UserManager<Pengguna>> _mockUserManager;

//     public AuthControllerTests()
//     {
//         var store = new Mock<IUserStore<Pengguna>>();
//         _mockUserManager = new Mock<UserManager<Pengguna>>(
//             store.Object, null, null, null, null, null, null, null, null);
//         Console.WriteLine("mock");
//         Console.WriteLine(_mockUserManager.Object.CreateAsync());
//         _controller = new AuthController(_mockUserManager.Object);
//     }

//     [Fact]
//     public async Task Register_ValidRequest_ReturnsOk()
//     {
//         var request = new RegisterForm
//         {
//             nama = "John Doe",
//             email = "john@example.com",
//             password = "P@ssword123"
//         };

//         var result = await _controller.Register(_db, request);

//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         var response = Assert.IsType<Dictionary<string, object>>(okResult.Value);
//         var newUser = (Pengguna)response["User"];

//         Assert.Equal(newUser.Nama, request.nama);
//         Assert.Equal(newUser.UserName, request.email);
//     }

//     [Fact]
//     public async Task Register_InvalidModelState_ReturnsBadRequest()
//     {
//         var request = new RegisterForm
//         {
//             nama = null,
//             email = "john@example.com",
//             password = "P@ssword123"
//         };

//         var result = await _controller.Register(_db, request);

//         // Assert
//         var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
//     }
// }
