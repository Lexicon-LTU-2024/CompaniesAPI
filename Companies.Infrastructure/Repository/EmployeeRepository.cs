using Companies.Infrastructure.Data;
using Domain.Contracts;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Infrastructure.Repository;
public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(DBContext db) :base(db)
    {
    }

    public async Task<Employee?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
    {
        return await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                                    .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges)
    {
        return await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                                    .ToListAsync();
    }
}
