using Companies.Infrastructure.Data;
using Companies.Infrastructure.Repository;
using Domain.Contracts;
using Service.Contracts;
using Service;

namespace Companies.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddPolicy("AllowAll", p =>
                p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

        });
    }

    public static void ConfigureSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DBContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DBContext") ?? throw new InvalidOperationException("Connection string 'DBContext' not found.")));
    }


    public static void ConfigureOpenApi(this IServiceCollection services) => services.AddEndpointsApiExplorer()
                                                                                     .AddSwaggerGen();


    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped(provider => new Lazy<ICompanyService>(() => provider.GetRequiredService<ICompanyService>()));
        services.AddScoped(provider => new Lazy<IEmployeeService>(() => provider.GetRequiredService<IEmployeeService>()));
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped(provider => new Lazy<ICompanyRepository>(() => provider.GetRequiredService<ICompanyRepository>()));
        services.AddScoped(provider => new Lazy<IEmployeeRepository>(() => provider.GetRequiredService<IEmployeeRepository>()));

    }


}
