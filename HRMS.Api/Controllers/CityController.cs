using HRMS.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly HRMSDbContext _context;

        public CityController(HRMSDbContext context)
        {
            _context = context;
        }

        // GET: api/City/5
        [HttpGet("{stateId}")]
        public async Task<IActionResult> GetCities(int stateId)
        {
            var cities = await _context.Cities
                .Where(c => c.StateId == stateId)
                .OrderBy(c => c.CityName)
                .ToListAsync();

            return Ok(cities);
        }
    }
}
