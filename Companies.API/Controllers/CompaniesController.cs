using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;


namespace Companies.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly DBContext db;
        private readonly IMapper mapper;

        public CompaniesController(DBContext context, IMapper mapper)
        {
            db = context;
            this.mapper = mapper;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany()
        {
            //OBS!!!!! IEnumerable!!!!
            //var companies = await db.Companies.ToListAsync();
            //IEnumerable<CompanyDto> dtos = mapper.Map<IEnumerable<CompanyDto>>(companies);

            IEnumerable<CompanyDto> companyDtos = await db.Companies.ProjectTo<CompanyDto>(mapper.ConfigurationProvider).ToListAsync();

            return Ok(companyDtos);
        }

      
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
        {
            var company = await db.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<CompanyDto>(company);

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
