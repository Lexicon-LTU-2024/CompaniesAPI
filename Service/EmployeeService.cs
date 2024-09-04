using AutoMapper;
using Companies.Shared.DTOs;
using Domain.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;
public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork uow, IMapper mapper)
    {
         _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges = false)
    {
        var companyExists = await _uow.Company.GetCompanyAsync(companyId, trackChanges);

        if (companyExists is null) return null!; //ToDo: Fix later

        var employees = await _uow.Employee.GetEmployeesAsync(companyId, false);

        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }
}
