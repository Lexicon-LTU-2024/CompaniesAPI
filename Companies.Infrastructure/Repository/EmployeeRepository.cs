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
public class EmployeeRepository : RepositoryBase<ApplicationUser>, IEmployeeRepository
{
    public EmployeeRepository(DBContext db) :base(db)
    {
    }

    public async Task<ApplicationUser?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
    {
        return await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                                    .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> GetEmployeesAsync(Guid companyId, bool trackChanges)
    {
        return await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                                    .ToListAsync();
    }
}
