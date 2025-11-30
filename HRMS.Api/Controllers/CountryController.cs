using HRMS.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly HRMSDbContext _context;

        public CountryController(HRMSDbContext context)
        {
            _context = context;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _context.Countries
                .OrderBy(c => c.CountryName)
                .ToListAsync();

            return Ok(countries);
        }
    }
}
