
using Companies.API.Paging;
using Companies.Shared.DTOs;
using Companies.Shared.Request;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<(IEnumerable<CompanyDto> companyDtos, MetaData metaData)> GetCompaniesAsync(CompanyRequestParams companyRequestParams, bool trackChanges = false);
    Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges = false);
}
