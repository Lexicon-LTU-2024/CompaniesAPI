using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Companies.API.Extensions;
using Companies.Infrastructure.Data;
using Companies.Infrastructure.Repository;
using Domain.Contracts;

namespace Companies.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureSql(builder.Configuration);
            builder.Services.AddControllers(configure => configure.ReturnHttpNotAcceptable = true)
                           // .AddXmlDataContractSerializerFormatters()
                            .AddNewtonsoftJson();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.ConfigureCors();
            builder.Services.ConfigureOpenApi();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                await app.SeedDataAsync();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
