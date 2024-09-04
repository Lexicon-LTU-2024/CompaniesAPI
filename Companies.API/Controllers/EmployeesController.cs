using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Companies.Infrastructure.Data;
using Domain.Contracts;
using Companies.Infrastructure.Repository;

namespace Companies.API.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EmployeesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee(Guid companyId)
        {

            var companyExists = await _uow.Company.GetCompanyAsync(companyId, false);

            if (companyExists is null) return NotFound();

            var employees = await _uow.Employee.GetEmployeesAsync(companyId, false);

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Ok(employeeDtos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid companyId, Guid id)
        {
            var companyExists = await _uow.Company.GetCompanyAsync(companyId, false);


            if (companyExists is null) return new NotFoundObjectResult("Company not found");

            var employee = await _uow.Employee.GetEmployeeAsync(companyId, id, false);
            if (employee == null) return NotFound();

            _uow.Employee.Delete(employee);
            await _uow.CompleteAsync();

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> PatchEmployee(Guid companyId, Guid id, JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        {
            if(patchDocument is null) return BadRequest("No patch doc");

            //var companyExists = await _db.Companies.AnyAsync(c => c.Id == companyId);

            //if(!companyExists) return NotFound();

            var employeeToPatch = await _uow.Employee.GetEmployeeAsync(companyId, id, true);
            if (employeeToPatch is null) return NotFound();


            var dto = _mapper.Map<EmployeeUpdateDto>(employeeToPatch);

            patchDocument.ApplyTo(dto, ModelState);
            TryValidateModel(dto);

            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            _mapper.Map(dto, employeeToPatch);
            await _uow.CompleteAsync();

            return NoContent();
        }
    }
}
