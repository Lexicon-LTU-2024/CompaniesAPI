using AutoMapper;
using Companies.Shared.DTOs;
using Domain.Contracts;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.TestControllersOnlyForDemo;

[Route("api/repo")]
[ApiController]
public class RepositoryController : ControllerBase
{
    private readonly ICompanyRepository companyRepository;
    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;

    public RepositoryController(ICompanyRepository companyRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        this.companyRepository = companyRepository;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees = false)
    {

        var user = await userManager.GetUserAsync(User);
        if (user is null) ArgumentNullException.ThrowIfNull(user);

        var companies = await companyRepository.GetCompaniesAsync(includeEmployees);
        var companiesDtos = mapper.Map<IEnumerable<CompanyDto>>(companies);

        return Ok(companiesDtos);
    }
}