using AutoMapper;
using Companies.Shared.DTOs;
using Companies.Shared.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
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
        private readonly IServiceManager serviceManager;
        private readonly IMapper mapper;

        public IntigrationController(IServiceManager serviceManager, IMapper mapper)
        {
            this.serviceManager = serviceManager;
            this.mapper = mapper;
        }

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
        
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> Index3()
        {
            var companies = await serviceManager.CompanyService.GetCompaniesAsync(new CompanyRequestParams(), false);
            var dtos = mapper.Map<IEnumerable<CompanyDto>>(companies);  
            return Ok(dtos);
        }


    }
}
