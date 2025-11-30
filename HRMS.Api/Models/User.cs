namespace HRMS.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }        
        public int? EmployeeId { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
