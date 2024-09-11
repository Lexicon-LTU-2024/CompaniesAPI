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
    //private readonly IServiceManager _serviceManager;
    //private readonly UserManager<ApplicationUser> userM;

    public SimpleController(/*IServiceManager serviceManager, UserManager<ApplicationUser> userM*/)
    {
        //_serviceManager = serviceManager;
        //this.userM = userM;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees = false)
    {
        //var user = User;
        //var username = userM.GetUserName(User);

        //var user1 = await userM.GetUserAsync(User);
        //var user2 = await userM.FindByIdAsync(user1.Id);
        //var user3 = await userM.FindByNameAsync(user1.UserName);
        //var isInRole = await userM.IsInRoleAsync(user1, "Admin");


        // var companyDtos = await _serviceManager.CompanyService.GetCompaniesAsync(includeEmployees);

        //if (User.Identity.IsAuthenticated)
        //{
        //    return Ok("is auth");
        //}
        //else
        //{
        //    return Ok("is not");
        //}
        return Ok();
        // return Ok(companyDtos);
    }



}