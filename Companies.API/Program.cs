﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Companies.API.Extensions;
using Companies.Infrastructure.Data;
using Companies.Infrastructure.Repository;
using Domain.Contracts;
using Service;
using Companies.Presentation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Claims;

namespace Companies.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureSql(builder.Configuration);
            builder.Services.AddControllers(configure =>
            {
                configure.ReturnHttpNotAcceptable = true;

                //var policy = new AuthorizationPolicyBuilder()
                //                    .RequireAuthenticatedUser()
                //                    .RequireRole("Employee")
                //                    .Build();

                //configure.Filters.Add(new AuthorizeFilter(policy));

            })
                            .AddNewtonsoftJson()
                            .AddApplicationPart(typeof(AssemblyReference).Assembly);

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.ConfigureCors();
            builder.Services.ConfigureOpenApi();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.ConfigureServices();
            builder.Services.ConfigureRepositories();
            builder.Services.ConfigureJwt(builder.Configuration);


            builder.Services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
               
            })
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<DBContext>()
               .AddDefaultTokenProviders();


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("Admin")
                          .RequireClaim(ClaimTypes.NameIdentifier)
                          .RequireClaim(ClaimTypes.Role));

                options.AddPolicy("EmployeePolicy", policy =>
                    policy.RequireRole("Employee"));
            });




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.ConfigureExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                await app.SeedDataAsync();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
