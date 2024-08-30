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
        private readonly DBContext _db;
        private readonly IMapper _mapper;

        public CompaniesController(DBContext context, IMapper mapper)
        {
            _db = context;
            this._mapper = mapper;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany()
        {
            //OBS!!!!! IEnumerable!!!!
            //var companies = await db.Companies.ToListAsync();
            //IEnumerable<CompanyDto> dtos = mapper.Map<IEnumerable<CompanyDto>>(companies);

            IEnumerable<CompanyDto> companyDtos = await _db.Companies.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider).ToListAsync();

            return Ok(companyDtos);
        }

      
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
        {
            var company = await _db.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<CompanyDto>(company);

            return Ok(dto);
         }

        //// PUT: api/Companies/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, CompanyUpdateDto dto)
        {
            if (id != dto.Id)  return BadRequest();

            var existingCompany = await _db.Companies.FindAsync(id);

            if(existingCompany is null) return NotFound();

            _mapper.Map(dto, existingCompany);
            await _db.SaveChangesAsync();

            return Ok(_mapper.Map<CompanyDto>(existingCompany)); //For demo!
        }


        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(CompanyCreateDto dto)
        {
            var company = _mapper.Map<Company>(dto);
            _db.Companies.Add(company);
            await _db.SaveChangesAsync();

            var createdCompanyDto = _mapper.Map<CompanyDto>(company);

            return CreatedAtAction(nameof(GetCompany), new { id = createdCompanyDto.Id }, createdCompanyDto);
        }

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
