using AutoMapper;
using Companies.API.Paging;
using Companies.Shared.DTOs;
using Companies.Shared.Request;
using Domain.Contracts;
using Domain.Models.Exeptions;
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

    public async Task<(IEnumerable<CompanyDto> companyDtos, MetaData metaData)> GetCompaniesAsync(CompanyRequestParams companyRequestParams, bool includeEmployees, bool trackChanges = false)
    {
        var commpaniesWithMetaData = await _uow.Company.GetCompaniesAsync(companyRequestParams, trackChanges, includeEmployees);
        var companyDtos =  _mapper.Map<IEnumerable<CompanyDto>>(commpaniesWithMetaData.Companies);
        return (companyDtos, commpaniesWithMetaData.MetaData);
    }

    public async Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges = false)
    {
        var company = await _uow.Company.GetCompanyAsync(id, trackChanges: false);

        if (company == null) throw new CompanyNotFoundException(id);

        return _mapper.Map<CompanyDto>(company);
    }
}
