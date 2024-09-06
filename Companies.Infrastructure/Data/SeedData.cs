using Bogus;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Infrastructure.Data
{
    public static class SeedData
    {
        private static UserManager<ApplicationUser> userManager = null!;
        private static RoleManager<IdentityRole> roleManager = null!;
        private static IConfiguration configuration = null!;
        private const string employeeRole = "Employee";
        private const string adminRole = "Admin";

        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var servicesProvider = scope.ServiceProvider;

                var db = servicesProvider.GetRequiredService<DBContext>();
                if (await db.Companies.AnyAsync()) return;

                userManager = servicesProvider.GetRequiredService<UserManager<ApplicationUser>>();
                roleManager = servicesProvider.GetRequiredService<RoleManager<IdentityRole>>();
                configuration = servicesProvider.GetRequiredService<IConfiguration>();

                //Null check on services!
                //await db.Database.MigrateAsync();


                try
                {
                    await CreateRolesAsync(new[] {adminRole, employeeRole});
                    var companies = GenerateCompanies(4);
                    await db.AddRangeAsync(companies);
                    await GenerateEmployeesAsync(30, companies.ToList());
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }

        private static async Task CreateRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static IEnumerable<Company> GenerateCompanies(int nrOfCompanies)
        {
            var faker = new Faker<Company>("sv").Rules((f, c) =>
            {
                c.Name = f.Company.CompanyName();
                c.Address = $"{f.Address.StreetAddress()}, {f.Address.City()}";
                c.Country = f.Address.Country();
               // c.Employees = GenerateEmployees(f.Random.Int(min: 2, max: 10));
            });

            return faker.Generate(nrOfCompanies);
        }

        private static async Task GenerateEmployeesAsync(int nrOfEmplyees, List<Company> companies)
        {
            string[] positions = ["Developer", "Tester", "Manager"];

            var faker = new Faker<ApplicationUser>("sv").Rules((f, e) =>
            {
                e.Name = f.Person.FullName;
                e.Age = f.Random.Int(min: 18, max: 70);
                e.Position = positions[f.Random.Int(0, positions.Length - 1)];
                e.Email = f.Person.Email;
                e.UserName = f.Person.UserName;
                e.Company = companies[f.Random.Int(0, companies.Count - 1)];
            });

            var users =  faker.Generate(nrOfEmplyees);

            var passWord = configuration["password"];
            if (string.IsNullOrEmpty(passWord))
                throw new Exception("password not exist in config");

            foreach (var user in users)
            {
                var result = await userManager.CreateAsync(user, passWord);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

                if (user.Position == "Manager")
                {
                    await userManager.AddToRoleAsync(user, adminRole);
                }
                else
                {
                    await userManager.AddToRoleAsync(user, employeeRole);
                }
            }
        }
    }
}
