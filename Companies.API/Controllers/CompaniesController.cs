using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Companies.Infrastructure.Data;
using Companies.Infrastructure.Repository;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;


namespace Companies.API.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public CompaniesController(IMapper mapper, IUnitOfWork uow )
    {
       
        _mapper = mapper;
        _uow = uow;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees)
    {

        //var dto = await _db.Companies.Select(c => new CompanyDto { Address = c.Address, Name = c.Name, Id = c.Id }).ToListAsync();
        //var dto2 = await _db.Companies.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider).ToListAsync();
        //var dto3 = await _mapper.ProjectTo<CompanyDto>(_db.Companies).ToListAsync();

        var companyDtos = includeEmployees ? _mapper.Map<IEnumerable<CompanyDto>>(await _uow.Company.GetCompaniesAsync(trackChanges: false, includeEmployees: true))
                                           : _mapper.Map<IEnumerable<CompanyDto>>(await _uow.Company.GetCompaniesAsync(trackChanges: false));
        return Ok(companyDtos);
    }

  

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
    {
        var company = await _uow.Company.GetCompanyAsync(id, trackChanges: false);

        if (company == null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<CompanyDto>(company);

        return Ok(dto);
    }

  

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(Guid id, CompanyUpdateDto dto)
    {
        if (id != dto.Id)  return BadRequest();

        var existingCompany = await _uow.Company.GetCompanyAsync(id, trackChanges: true);

        if(existingCompany is null) return NotFound();

        _mapper.Map(dto, existingCompany);
        await _uow.CompleteAsync();

        return Ok(_mapper.Map<CompanyDto>(existingCompany)); //For demo!
    }

    [HttpPost]
    public async Task<ActionResult<Company>> PostCompany(CompanyCreateDto dto)
    {
        var company = _mapper.Map<Company>(dto);
        await _uow.Company.CreateAsync(company);
        await _uow.CompleteAsync();

        var createdCompanyDto = _mapper.Map<CompanyDto>(company);

        return CreatedAtAction(nameof(GetCompany), new { id = createdCompanyDto.Id }, createdCompanyDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        var company = await _uow.Company.GetCompanyAsync(id, trackChanges: false);
        if (company == null) return NotFound();

        _uow.Company.Delete(company);
        await _uow.CompleteAsync();

        return NoContent();
    }
}
