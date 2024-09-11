using Companies.Presentation.TestControllersOnlyForDemo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Controller.Tests;

public class SimpleControllerTests
{
    [Fact]
    public async Task GetCompany_ShouldReturn200OK()
    {
        var sut = new SimpleController();

        var result = await sut.GetCompany();
        var resultType = result.Result as OkResult;

        Assert.IsType<OkResult>(resultType);
        Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
    }

    [Fact]
    public async Task GetComapany_IfNotAuthenticated_ShouldReturn400BadRequest() 
    {
        //Arrange
        var httpContext = new Mock<HttpContext>();
        httpContext.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(false);

        var controllerContext = new ControllerContext
        {
            HttpContext = httpContext.Object
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