using Domain.Models.Entities;

namespace Domain.Contracts;
public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetCompaniesAsync(bool trackChanges, bool includeEmployees = false);
    Task<Company?> GetCompanyAsync(Guid id);

    Task CreateAsync(Company company);
    void Update(Company company);
    void Delete(Company company);
}