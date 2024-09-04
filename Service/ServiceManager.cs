using AutoMapper;
using Domain.Contracts;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;

    public ICompanyService CompanyService => _companyService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;

    public ServiceManager(IUnitOfWork uow, IMapper mapper)
    {
        if (uow is null)
        {
            throw new ArgumentNullException(nameof(uow));
        }

        _companyService = new Lazy<ICompanyService>(() => new CompanyService(uow, mapper));
        _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(uow, mapper));
    }
}
