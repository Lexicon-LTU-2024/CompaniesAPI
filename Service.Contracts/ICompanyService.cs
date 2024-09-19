
using Companies.API.Paging;
using Companies.Shared.DTOs;
using Companies.Shared.Request;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<(IEnumerable<CompanyDto> companyDtos, MetaData metaData)> GetCompaniesAsync(CompanyRequestParams companyRequestParams,bool includeEmployees, bool trackChanges = false);
    Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges = false);
}
