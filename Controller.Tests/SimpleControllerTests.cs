using Companies.Presentation.TestControllersOnlyForDemo;
using Companies.Shared.DTOs;
using Controller.Tests.Extensions;
using Controller.Tests.Fixtures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Controller.Tests;

[Collection("DatabaseCollection")]
public class SimpleControllerTests //: IClassFixture<DataBaseFixture>
{
    private readonly DataBaseFixture fixture;

    public SimpleControllerTests(DataBaseFixture fixture)
    {
        this.fixture = fixture;
    }


    [Fact]
    public async Task GetCompany_ShouldReturn400BadREquest()
    {
        var sut = new SimpleController(fixture.Context, fixture.Mapper);
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
        var sut = new SimpleController(fixture.Context, fixture.Mapper);
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

        var sut = new SimpleController(fixture.Context, fixture.Mapper);
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

    [Fact]
    public async Task GetCompanies_ShouldReturn200Ok()
    {
        var nrOfCompanies = fixture.Context.Companies.Count();
        var sut = new SimpleController(fixture.Context, fixture.Mapper);

        var result = await sut.GetCompany2();

         var okRes = Assert.IsType<OkObjectResult>(result.Result);
         var companiesFromSut = Assert.IsType<List<CompanyDto>>(okRes.Value);
         Assert.Equal(nrOfCompanies, companiesFromSut.Count);
    }

}