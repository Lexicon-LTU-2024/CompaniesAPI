using AutoMapper;
using Companies.Infrastructure.Data;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Tests.Fixtures;
public class DataBaseFixture : IDisposable
{
    public Mapper Mapper { get; }
    public DBContext Context { get; }

    public DataBaseFixture()
    {
        Mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        }));

        //var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //var builder = new DbContextOptionsBuilder().UseSqlServer(configuration.GetConnectionString("sqlConnection"));

        var options = new DbContextOptionsBuilder<DBContext>()
         .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDataBase2;Trusted_Connection=True;MultipleActiveResultSets=true")
         .Options;

        var context = new DBContext(options);

        context.Database.Migrate();

        context.Companies.AddRange(
                                [
                                     new Company()
                                     {
                                         Name = "TestCompanyName",
                                         Address = "TestAdress",
                                         Country = "TestCountry",
                                         Employees =
                                             [
                                                 new ApplicationUser
                                                 {
                                                     UserName = "TestUserName",
                                                     Email = "test@test.com",
                                                     Age = 50,
                                                     Name = "TestName",
                                                     Position = "TestPosition"
                                                 }
                                             ]
                                     }
                                ]);

        context.SaveChanges();
        Context = context;

    }

  

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
