using Domain.Models.Entities;

namespace Companies.Infrastructure.Repository;
public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetCompaniesAsync(bool includeEmployees = false);
    Task<Company?> GetCompanyAsync(Guid id);
}