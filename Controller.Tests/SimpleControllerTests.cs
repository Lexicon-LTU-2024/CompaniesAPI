using Companies.Presentation.TestControllersOnlyForDemo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.CompilerServices;

namespace Controller.Tests;

public class SimpleControllerTests
{
    [Fact]
    public async Task GetCompany_ShouldReturn400BadREquest()
    {
        var sut = new SimpleController();

        var result = await sut.GetCompany();
        var resultType = result.Result as BadRequestObjectResult;

        Assert.IsType<BadRequestObjectResult>(resultType);
        Assert.Equal(StatusCodes.Status400BadRequest, resultType.StatusCode);
    }

    [Fact]
    public async Task GetComapany_IfNotAuthenticated_ShouldReturn400BadRequest() 
    {
        //Arrange
        //var httpContext = new Mock<HttpContext>();
        //httpContext.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(false);
        var httpContext = Mock.Of<HttpContext>(x => x.User.Identity.IsAuthenticated == false);

        var controllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        var sut = new SimpleController();
        sut.ControllerContext = controllerContext;

        //Act
        var res = await sut.GetCompany(false);
        var resultType = res.Result as BadRequestObjectResult;

        //Assert
        Assert.IsType<BadRequestObjectResult>(resultType);
        Assert.Equal(resultType.Value, "is not auth");


    }
}