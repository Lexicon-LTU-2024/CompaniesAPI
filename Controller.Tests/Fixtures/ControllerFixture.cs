using AutoMapper;
using Companies.Infrastructure.Data;
using Companies.Presentation.TestControllersOnlyForDemo;
using Domain.Contracts;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Tests.Fixtures;
public class ControllerFixture : IDisposable
{
    public Mock<ICompanyRepository> MockRepo;
    public RepositoryController Sut;
    public Mock<UserManager<ApplicationUser>> UserManager;

    public ControllerFixture()
    {
        MockRepo = new Mock<ICompanyRepository>();
        var mockUoW = new Mock<IUnitOfWork>();
        mockUoW.Setup(u => u.Company).Returns(MockRepo.Object);

        var mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        }));

        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        UserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        Sut = new RepositoryController(mockUoW.Object, mapper, UserManager.Object);
    }


    public List<Company> GetCompanies()
    {

        return new List<Company>
            {
                new Company
                {
                     Id = Guid.NewGuid(),
                     Name = "Test",
                     Address = "Ankeborg, Sweden",
                     Employees = new List<ApplicationUser>()
                },
                 new Company
                {
                     Id = Guid.NewGuid(),
                     Name = "Test",
                     Address = "Ankeborg, Sweden",
                     Employees = new List<ApplicationUser>()
                }
            };


    }
    public void Dispose()
    {
        //Not used here
    }
}
