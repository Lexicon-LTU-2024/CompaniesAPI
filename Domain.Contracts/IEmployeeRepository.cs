using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IEmployeeRepository 
    {
        Task<ApplicationUser?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        Task<IEnumerable<ApplicationUser>> GetEmployeesAsync(Guid companyId, bool trackChanges);
        Task CreateAsync(ApplicationUser employee);
        void Update(ApplicationUser employee);
        void Delete(ApplicationUser employee);
    }
}
