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

    public ICompanyRepository Company { get; }
    public IEmployeeRepository Employee { get; set; }
    public UnitOfWork(DBContext db)
    {
        Company = new CompanyRepository(db);
        Employee = new EmployeeRepository(db);
        _db = db;
    }

    public async Task CompleteAsync()
    {
        await _db.SaveChangesAsync();
    }
}
