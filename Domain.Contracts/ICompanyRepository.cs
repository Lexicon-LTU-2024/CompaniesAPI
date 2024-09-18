using Companies.Shared.Request;
using Domain.Models.Entities;

namespace Domain.Contracts;
public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetCompaniesAsync(CompanyRequestParams companyRequestParams, bool trackChanges, bool includeEmployees = false);
    Task<Company?> GetCompanyAsync(Guid id, bool trackChanges);

    Task CreateAsync(Company company);
    void Update(Company company);
    void Delete(Company company);
}