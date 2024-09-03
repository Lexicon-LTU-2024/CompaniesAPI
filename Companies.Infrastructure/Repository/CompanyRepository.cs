using Companies.Infrastructure.Data;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Infrastructure.Repository;
public class CompanyRepository : ICompanyRepository
{
    private readonly DBContext _db;

    public CompanyRepository(DBContext db)
    {
        _db = db;
    }

    public async Task<Company?> GetCompanyAsync(Guid id)
    {
        return await _db.Companies.FirstOrDefaultAsync(c => c.Id.Equals(id));
    }

    public async Task<IEnumerable<Company>> GetCompaniesAsync(bool includeEmployees = false)
    {
        return includeEmployees ? await _db.Companies.Include(c => c.Employees).ToListAsync() :
                                  await _db.Companies.ToListAsync();
    }
}
