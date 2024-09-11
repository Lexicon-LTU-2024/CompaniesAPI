using Companies.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
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
   
    public SimpleController()
    {
      
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees = false)
    {
        if (User.Identity.IsAuthenticated)
        {
            return Ok("is auth");
        }
        else
        {
            return BadRequest("is not auth");
        }
      //  return Ok();
     
    }



}