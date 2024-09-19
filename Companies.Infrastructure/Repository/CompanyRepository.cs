using Companies.API.Paging;
using Companies.Infrastructure.Data;
using Companies.Shared.Request;
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

    public async Task<PagedList<Company>> GetCompaniesAsync(CompanyRequestParams companyRequestParams, bool trackChanges)
    {
        var companies = companyRequestParams.IncludeEmployees ?   FindAll(trackChanges)
                                                                     .Include(c => c.Employees)
                                                                     :
                                                                  FindAll(trackChanges);
                                            

        return await PagedList<Company>.CreateAsync(companies, companyRequestParams.PageNumber, companyRequestParams.PageSize);
    }
}
