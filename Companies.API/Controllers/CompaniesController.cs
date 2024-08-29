using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Companies.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly DBContext db;

        public CompaniesController(DBContext context)
        {
            db = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany()
        {
            var companies = db.Companies;
            var companyDtos = companies.Select(c => new CompanyDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Country = c.Country
            });

            return Ok(await companyDtos.ToListAsync());
        }

      
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
        {
            var company = await db.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            var dto = new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };

            return Ok(dto);
         }

            //// PUT: api/Companies/5
            //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            //[HttpPut("{id}")]
            //public async Task<IActionResult> PutCompany(Guid id, Company company)
            //{
            //    if (id != company.Id)
            //    {
            //        return BadRequest();
            //    }

            //    _context.Entry(company).State = EntityState.Modified;

            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CompanyExists(id))
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

            //// POST: api/Companies
            //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            //[HttpPost]
            //public async Task<ActionResult<Company>> PostCompany(Company company)
            //{
            //    _context.Company.Add(company);
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetCompany", new { id = company.Id }, company);
            //}

            //// DELETE: api/Companies/5
            //[HttpDelete("{id}")]
            //public async Task<IActionResult> DeleteCompany(Guid id)
            //{
            //    var company = await _context.Company.FindAsync(id);
            //    if (company == null)
            //    {
            //        return NotFound();
            //    }

            //    _context.Company.Remove(company);
            //    await _context.SaveChangesAsync();

            //    return NoContent();
            //}

            //private bool CompanyExists(Guid id)
            //{
            //    return _context.Company.Any(e => e.Id == id);
            //}
        }
}
