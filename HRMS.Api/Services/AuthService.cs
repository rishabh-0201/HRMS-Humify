using HRMS.Api.Data;
using HRMS.Api.Helpers;
using HRMS.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Api.Services
{
    public class AuthService 
    {

        private readonly HRMSDbContext _db;

        private readonly TokenService _tokenService;

        public AuthService(HRMSDbContext db, TokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        

        public async Task<User?> RegisterCompanyAdminAsync(string companyName, string companyCode, string userName, string password)
        {
            // 1. Check if company exists
            var existingCompany = await _db.Companies.FirstOrDefaultAsync(c => c.CompanyCode == companyCode);
            if (existingCompany != null)
                return null;

            // 2. Create company
            var company = new Company
            {
                CompanyName = companyName,
                CompanyCode = companyCode
            };
            await _db.Companies.AddAsync(company);
            await _db.SaveChangesAsync();

            // 3. Ensure Admin role exists
            var adminRole = await _db.Roles.FirstOrDefaultAsync(r => r.RoleName == "Admin");

            if (adminRole == null)
            {
                adminRole = new Role { RoleName = "Admin" };
                await _db.Roles.AddAsync(adminRole);
                await _db.SaveChangesAsync();
            }

            // 4. Create admin user
            var user = new User
            {
                CompanyId = company.CompanyId,
                RoleId = adminRole.RoleId,
                Username = userName,
                PasswordHash = PasswordHelper.HashPassword(password)
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return user;
        }


        // Login

        public async Task<User?> ValidateUserAsync(string companyCode, string username, string password)
        {

            var user = await _db.Users
                .Include(u => u.Company)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Company.CompanyCode == companyCode &&
                u.Username == username);

            if (user == null)
            {
                return null;
            }

            if (!PasswordHelper.VerifyPassword(password, user.PasswordHash))
                return null;

            return user;
        }
    }
}
