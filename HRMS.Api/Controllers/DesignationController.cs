using HRMS.Api.Data;
using HRMS.Api.DTOs;
using HRMS.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class DesignationController : ControllerBase
{
    private readonly HRMSDbContext _db;

    public DesignationController(HRMSDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetDesignations()
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        var designations = await _db.Designations
            .Where(x => x.IsActive && x.CompanyId == companyId)
            .OrderBy(x => x.DesignationId)
            .ToListAsync();

        return Ok(designations);
    }

    // Get By Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        var dept = await _db.Designations
            .FirstOrDefaultAsync(x => x.DesignationId == id && x.CompanyId == companyId && x.IsActive);

        if (dept == null)
            return NotFound();

        return Ok(dept);
    }


    // CREATE
    [HttpPost]
    public async Task<IActionResult> CreateDesignation([FromBody] DesignationDto dto)
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        // Get last designation companywise
        var lastId = await _db.Designations
            .Where(d => d.CompanyId == companyId)
            .OrderByDescending(d => d.DesignationId)
            .Select(d => d.DesignationId)
            .FirstOrDefaultAsync();

        var newDesign = new Designation
        {
            CompanyId = companyId,
            DesignationId = lastId + 1,
            DesignationName = dto.DesignationName,
            IsActive = true,
            CreatedOn = DateTime.Now
        };

        _db.Designations.Add(newDesign);
        await _db.SaveChangesAsync();

        return Ok(newDesign);
    }


    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DesignationDto dto)
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        var design = await _db.Designations
            .FirstOrDefaultAsync(x => x.DesignationId == id && x.CompanyId == companyId);

        if (design == null)
            return NotFound();

        design.DesignationName = dto.DesignationName;
        design.ModifiedOn = DateTime.Now;

        await _db.SaveChangesAsync();

        return Ok(design);
    }


    // SOFT DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        var design = await _db.Designations
            .FirstOrDefaultAsync(x => x.DesignationId == id && x.CompanyId == companyId);

        if (design == null)
            return NotFound();

        design.IsActive = false;
        design.ModifiedOn = DateTime.Now;

        await _db.SaveChangesAsync();

        return Ok(new { message = "Designation deactivated" });
    }

}
