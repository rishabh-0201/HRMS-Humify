using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Api.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

       
    }
}
