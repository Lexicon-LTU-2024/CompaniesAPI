using Companies.Shared.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companies.API.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController :  ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly UserManager<ApplicationUser> userManager;

    public CompaniesController(IServiceManager serviceManager, UserManager<ApplicationUser> userManager)
    {
        _serviceManager = serviceManager;
        this.userManager = userManager;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees)
    {
        //var auth = User.Identity.IsAuthenticated;

        //var userName = userManager.GetUserName(User);
        //var user = await userManager.GetUserAsync(User);

        var companyDtos = await _serviceManager.CompanyService.GetCompaniesAsync(includeEmployees);
        return Ok(companyDtos);
    }



    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
    {
        var dto = await _serviceManager.CompanyService.GetCompanyAsync(id);

        return Ok(dto);
    }



    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutCompany(Guid id, CompanyUpdateDto dto)
    //{
    //    if (id != dto.Id)  return BadRequest();

    //    var existingCompany = await _uow.Company.GetCompanyAsync(id, trackChanges: true);

    //    if(existingCompany is null) return NotFound();

    //    _mapper.Map(dto, existingCompany);
    //    await _uow.CompleteAsync();

    //    return Ok(_mapper.Map<CompanyDto>(existingCompany)); //For demo!
    //}

    //[HttpPost]
    //public async Task<ActionResult<Company>> PostCompany(CompanyCreateDto dto)
    //{
    //    var company = _mapper.Map<Company>(dto);
    //    await _uow.Company.CreateAsync(company);
    //    await _uow.CompleteAsync();

    //    var createdCompanyDto = _mapper.Map<CompanyDto>(company);

    //    return CreatedAtAction(nameof(GetCompany), new { id = createdCompanyDto.Id }, createdCompanyDto);
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteCompany(Guid id)
    //{
    //    var company = await _uow.Company.GetCompanyAsync(id, trackChanges: false);
    //    if (company == null) return NotFound();

    //    _uow.Company.Delete(company);
    //    await _uow.CompleteAsync();

    //    return NoContent();
    //}
}
