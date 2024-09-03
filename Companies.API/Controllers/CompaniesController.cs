using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Companies.Infrastructure.Data;
using Companies.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;


namespace Companies.API.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly DBContext _db;
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;

    public CompaniesController(DBContext context, IMapper mapper, ICompanyRepository companyRepository)
    {
        _db = context;
        _mapper = mapper;
        _companyRepository = companyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees)
    {

        //var dto = await _db.Companies.Select(c => new CompanyDto { Address = c.Address, Name = c.Name, Id = c.Id }).ToListAsync();
        //var dto2 = await _db.Companies.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider).ToListAsync();
        //var dto3 = await _mapper.ProjectTo<CompanyDto>(_db.Companies).ToListAsync();

        var companyDtos = includeEmployees ? _mapper.Map<IEnumerable<CompanyDto>>(await GetCompaniesAsync(true))
                                           : _mapper.Map<IEnumerable<CompanyDto>>(await GetCompaniesAsync());
        return Ok(companyDtos);
    }

  

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
    {
        var company = await GetCompanyAsync(id);

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

        var existingCompany = await GetCompanyAsync(id);

        if(existingCompany is null) return NotFound();

        _mapper.Map(dto, existingCompany);
        await _db.SaveChangesAsync();

        return Ok(_mapper.Map<CompanyDto>(existingCompany)); //For demo!
    }

    [HttpPost]
    public async Task<ActionResult<Company>> PostCompany(CompanyCreateDto dto)
    {
        var company = _mapper.Map<Company>(dto);
        _db.Companies.Add(company);
        await _db.SaveChangesAsync();

        var createdCompanyDto = _mapper.Map<CompanyDto>(company);

        return CreatedAtAction(nameof(GetCompany), new { id = createdCompanyDto.Id }, createdCompanyDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        var company = await GetCompanyAsync(id);
        if (company == null) return NotFound();

        _db.Companies.Remove(company);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
