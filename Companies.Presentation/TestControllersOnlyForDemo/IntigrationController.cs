using Companies.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.TestControllersOnlyForDemo
{
    [Route("api/demo")]
    [ApiController]
    public class IntigrationController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Hello from controller");
        }

        [HttpGet("dto")]
        [AllowAnonymous]
        public ActionResult Index2()
        {
            var dto = new CompanyDto { Name = "Working" };
            return Ok(dto);
        }
    }
}
