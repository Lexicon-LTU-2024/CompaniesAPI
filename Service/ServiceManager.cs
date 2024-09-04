using Domain.Contracts;

namespace Service;

public class ServiceManager
{
    private readonly Lazy<CompanyService> _companyService;
    private readonly Lazy<EmployeeService> _employeeService;

    public CompanyService CompanyService => _companyService.Value;
    public EmployeeService EmployeeService => _employeeService.Value;

    public ServiceManager(IUnitOfWork uow)
    {
        if (uow is null)
        {
            throw new ArgumentNullException(nameof(uow));
        }

        _companyService = new Lazy<CompanyService>(() => new CompanyService(uow));
        _employeeService = new Lazy<EmployeeService>(() => new EmployeeService(uow));
    }
}
