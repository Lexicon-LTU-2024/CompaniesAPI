using AutoMapper;
using Domain.Contracts;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;
    private readonly Lazy<IAuthService> _authService;

    public ICompanyService CompanyService => _companyService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;
    public IAuthService AuthService => _authService.Value;

    public ServiceManager(Lazy<ICompanyService> companyService, Lazy<IEmployeeService> employeeService, Lazy<IAuthService> authService )
    {
        _companyService = companyService;
        _employeeService = employeeService;
        _authService = authService;
    }
}
