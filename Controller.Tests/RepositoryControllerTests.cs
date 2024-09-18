using AutoMapper;
using Companies.Infrastructure.Data;
using Companies.Presentation.TestControllersOnlyForDemo;
using Companies.Shared.DTOs;
using Controller.Tests.Fixtures;
using Domain.Contracts;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Tests;
public class RepositoryControllerTests : IClassFixture<ControllerFixture>
{
    private readonly ControllerFixture fixture;

    public RepositoryControllerTests(ControllerFixture controllerFixture)
    {
        this.fixture = controllerFixture;
    }

    [Fact]
    public async Task GetCompany_ShouldReturnAllCompanies()   
    {
        //Arrange
        var companies = fixture.GetCompanies();
        fixture.MockRepo.Setup(m => m.GetCompaniesAsync(null, false, It.IsAny<bool>())).ReturnsAsync(companies);
        fixture.UserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new ApplicationUser());

        //Act
        var result = await fixture.Sut.GetCompany(false);

        //Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var items = Assert.IsType<List<CompanyDto>>(okObjectResult.Value);
        Assert.Equal(items.Count, companies.Count);
    }

    [Fact]
    public async Task GetCompany_UserIsNull_ShouldThrowNullRefferenceException()
    {
        var companies = fixture.GetCompanies();
        fixture.MockRepo.Setup(m => m.GetCompaniesAsync(null, false, It.IsAny<bool>())).ReturnsAsync(companies);
        fixture.UserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(() => null);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await fixture.Sut.GetCompany(false));
    }
}


public interface IUserService
{
    Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal);
    Task<bool> IsInRoleAsync(ApplicationUser user, string role);
}

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
    {
        return await _userManager.GetUserAsync(principal);
    }

    public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
    {
        return await _userManager.IsInRoleAsync(user, role);
    }
}
