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
public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CompanyService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync(bool includeEmployees, bool trackChanges = false) =>
         includeEmployees ? _mapper.Map<IEnumerable<CompanyDto>>(await _uow.Company.GetCompaniesAsync(trackChanges, includeEmployees))
                          : _mapper.Map<IEnumerable<CompanyDto>>(await _uow.Company.GetCompaniesAsync(trackChanges));
    
}
