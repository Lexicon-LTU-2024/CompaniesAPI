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
        Task<Employee?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges);
        Task CreateAsync(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
