using Service.Contracts;

namespace Service;
public interface IServiceManager
{
    ICompanyService CompanyService { get; }
    IEmployeeService EmployeeService { get; }
    IAuthService AuthService { get; }
}