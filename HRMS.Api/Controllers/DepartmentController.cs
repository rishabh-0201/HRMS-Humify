using HRMS.Api.Data;
using HRMS.Api.DTOs;
using HRMS.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class DepartmentController : ControllerBase
{
    private readonly HRMSDbContext _db;

    public DepartmentController(HRMSDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        var departments = await _db.Departments
            .Where(x => x.IsActive && x.CompanyId == companyId)
            .OrderBy(x => x.DepartmentId)
            .ToListAsync();

        return Ok(departments);
    }

    // Get By Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        var dept = await _db.Departments
            .FirstOrDefaultAsync(x => x.DepartmentId == id && x.CompanyId == companyId && x.IsActive);

        if (dept == null)
            return NotFound();

        return Ok(dept);
    }


    // CREATE
    [HttpPost]
    public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto dto)
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        // Get last department id for this company
        var lastId = await _db.Departments
            .Where(d => d.CompanyId == companyId)
            .OrderByDescending(d => d.DepartmentId)
            .Select(d => d.DepartmentId)
            .FirstOrDefaultAsync();

        var newDept = new Department
        {
            CompanyId = companyId,
            DepartmentId = lastId + 1,
            DepartmentName = dto.DepartmentName,
            IsActive = true,
            CreatedOn = DateTime.Now
        };

        _db.Departments.Add(newDept);
        await _db.SaveChangesAsync();

        return Ok(newDept);
    }


    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DepartmentDto dto)
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        var dept = await _db.Departments
            .FirstOrDefaultAsync(x => x.DepartmentId == id && x.CompanyId == companyId);

        if (dept == null)
            return NotFound();

        dept.DepartmentName = dto.DepartmentName;
        dept.ModifiedOn = DateTime.Now;

        await _db.SaveChangesAsync();

        return Ok(dept);
    }


    // SOFT DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int companyId = int.Parse(User.Claims.First(c => c.Type == "CompanyId").Value);

        var dept = await _db.Departments
            .FirstOrDefaultAsync(x => x.DepartmentId == id && x.CompanyId == companyId);

        if (dept == null)
            return NotFound();

        dept.IsActive = false;
        dept.ModifiedOn = DateTime.Now;

        await _db.SaveChangesAsync();

        return Ok(new { message = "Department deactivated" });
    }

}
