using AutoMapper;
using Companies.Shared.DTOs;
using Companies.Shared.Request;
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
    private readonly IUnitOfWork uow;

    // private readonly ICompanyRepository companyRepository;
    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;

    public RepositoryController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        this.uow = unitOfWork;
        // this.companyRepository = companyRepository;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees = false)
    {
        var companies = await uow.Company.GetCompaniesAsync(new CompanyRequestParams(), trackChanges: false, includeEmployees);
        var companiesDtos = mapper.Map<IEnumerable<CompanyDto>>(companies);

        var user = await userManager.GetUserAsync(User);
        if (user is null) ArgumentNullException.ThrowIfNull(user);

        return Ok(companiesDtos);
    }
}