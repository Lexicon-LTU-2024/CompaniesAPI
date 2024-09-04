using Companies.Infrastructure.Data;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Infrastructure.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly DBContext _db;
    private readonly Lazy<ICompanyRepository> _companyRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;

    public ICompanyRepository Company => _companyRepository.Value;
    public IEmployeeRepository Employee => _employeeRepository.Value;

    public UnitOfWork(DBContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(db));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(db));
    }

    public async Task CompleteAsync()
    {
        await _db.SaveChangesAsync();
    }
}
