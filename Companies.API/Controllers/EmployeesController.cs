using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Companies.API.Data;
using Companies.API.Models.Entities;
using System.Data.Common;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace Companies.API.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DBContext _db;
        private readonly IMapper _mapper;

        public EmployeesController(DBContext context, IMapper mapper)
        {
            _db = context;
            this._mapper = mapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee(Guid companyId)
        {

            var companyExists = await _db.Companies.AnyAsync(c => c.Id == companyId);

            if(!companyExists) return NotFound();

            var employees = await _db.Employee.Where(e => e.CompanyId.Equals(companyId)).ToListAsync();

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Ok(employeeDtos);
        }

        // GET: api/Employees/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        //{
        //    var employee = await _context.Employee.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        //// PUT: api/Employees/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        //{
        //    if (id != employee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Employees
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    _context.Employee.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid companyId, Guid id)
        {
            var companyExists = await _db.Companies
                                    .AnyAsync(c => c.Id.Equals(companyId));

            if (!companyExists) return new NotFoundObjectResult("Company not found");

            var employee = await _db.Employee.FirstOrDefaultAsync(e => e.Id.Equals(id) && e.CompanyId.Equals(companyId));
            if (employee == null) return NotFound();

            _db.Employee.Remove(employee);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> PatchEmployee(Guid companyId, Guid id, JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        {
            if(patchDocument is null) return BadRequest("No patch doc");

            //var companyExists = await _db.Companies.AnyAsync(c => c.Id == companyId);

            //if(!companyExists) return NotFound();

            var employeeToPatch = await _db.Employee.FirstOrDefaultAsync(e => e.Id.Equals(id) && e.CompanyId.Equals(companyId));
            if (employeeToPatch is null) return NotFound();


            var dto = _mapper.Map<EmployeeUpdateDto>(employeeToPatch);

            patchDocument.ApplyTo(dto, ModelState);
            TryValidateModel(dto);

            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            _mapper.Map(dto, employeeToPatch);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
