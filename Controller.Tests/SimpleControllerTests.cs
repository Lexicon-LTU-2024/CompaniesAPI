using Companies.Presentation.TestControllersOnlyForDemo;
using Microsoft.AspNetCore.Mvc;

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
        Assert.Equal(200, resultType.StatusCode);
    }
}