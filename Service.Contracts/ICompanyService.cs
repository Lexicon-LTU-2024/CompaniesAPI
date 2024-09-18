
using Companies.Shared.DTOs;
using Companies.Shared.Request;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetCompaniesAsync(CompanyRequestParams companyRequestParams,bool includeEmployees, bool trackChanges = false);
    Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges = false);
}
