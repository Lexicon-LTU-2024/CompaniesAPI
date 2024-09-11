using Companies.Presentation.TestControllersOnlyForDemo;
using Controller.Tests.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Controller.Tests;

public class SimpleControllerTests
{
    [Fact]
    public async Task GetCompany_ShouldReturn400BadREquest()
    {
        var sut = new SimpleController();
        sut.SetUserIsAuthenticated(false);

        var result = await sut.GetCompany();
        var resultType = result.Result as BadRequestObjectResult;

        Assert.IsType<BadRequestObjectResult>(resultType);
        Assert.Equal(StatusCodes.Status400BadRequest, resultType.StatusCode);
    }

    [Fact]
    public async Task GetComapany_IfNotAuthenticated_ShouldReturn400BadRequest()
    {
        //Arrange
        var sut = new SimpleController();
        sut.SetUserIsAuthenticated(false);

        //Act
        var res = await sut.GetCompany(false);
        var resultType = res.Result as BadRequestObjectResult;

        //Assert
        Assert.IsType<BadRequestObjectResult>(resultType);
        Assert.Equal(resultType.Value, "is not auth");

    }

    [Fact]
    public async Task GetCompany_IfNotAuthenticated_ShouldReturn400BadRequest2()
    {
        var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
        mockClaimsPrincipal.SetupGet(c => c.Identity.IsAuthenticated).Returns(false);

        var sut = new SimpleController();
        sut.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
            {
                User = mockClaimsPrincipal.Object
            }
        };

        var result = await sut.GetCompany(false);
        var resultType = result.Result as BadRequestObjectResult;

        Assert.IsType<BadRequestObjectResult>(resultType);
        Assert.Equal(StatusCodes.Status400BadRequest, resultType.StatusCode);


    }
}