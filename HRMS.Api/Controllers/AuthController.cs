using HRMS.Api.DTOs;
using HRMS.Api.Models;
using HRMS.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        // POST: api/Auth/register

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCompanyDto dto)
        {
            try
            {
                var result = await _authService.RegisterCompanyAdminAsync(dto.CompanyName, dto.CompanyCode, dto.AdminUserName, dto.AdminPassword);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        { // common resp
            try
            {
                var result = await _authService.ValidateUserAsync(dto.CompanyCode, dto.UserName, dto.Password);                
                
                if (result == null)
                    return Unauthorized(new { message = "Invalid credentials" });
                
                var token = _tokenService.GenerateToken(result);

                return Ok(new
                {
                    username = result.Username,
                    roleName = result.Role.RoleName,
                    token
                });              
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
