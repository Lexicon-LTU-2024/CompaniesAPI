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
public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(DBContext db) : base(db){}

    public async Task<Company?> GetCompanyAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(c => c.Id.Equals(id), trackChanges)
                    .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Company>> GetCompaniesAsync(bool trackChanges, bool includeEmployees = false)
    {
        return includeEmployees ? await FindAll(trackChanges).Include(c => c.Employees).ToListAsync() :
                                  await FindAll(trackChanges).ToListAsync();
    }
}
