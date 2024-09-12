using AutoMapper;
using Companies.Infrastructure.Data;
using Companies.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.TestControllersOnlyForDemo;

[Route("api/simple")]
[ApiController]
public class SimpleController : ControllerBase
{
    private readonly DBContext db;
    private readonly IMapper mapper;

    public SimpleController(DBContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    //Fix for swagger need unique path
    [HttpGet("1")]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees = false)
    {
        if (User?.Identity?.IsAuthenticated ?? false)
        {
            return Ok("is auth");
        }
        else
        {
            return BadRequest("is not auth");
        }
      //  return Ok();
     
    }

    //Fix for swagger need unique path
    [HttpGet("2")]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany2()
    {
        var companies = await db.Companies.ToListAsync();
        var compDtos = mapper.Map<IEnumerable<CompanyDto>>(companies);  
        return Ok(compDtos);
     
    }
}