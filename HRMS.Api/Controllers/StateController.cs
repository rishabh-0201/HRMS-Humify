using HRMS.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HRMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly HRMSDbContext _context;

        public StateController(HRMSDbContext context)
        {
            _context = context;
        }

        // GET: api/State/5
        [HttpGet("{countryId}")]
        public async Task<IActionResult> GetStates(int countryId)
        {
            var states = await _context.States
                .Where(s => s.CountryId == countryId)
                .OrderBy(s => s.StateName)
                .ToListAsync();

            return Ok(states);
        }
    }
}
