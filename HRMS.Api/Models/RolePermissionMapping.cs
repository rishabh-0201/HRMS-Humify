using System.ComponentModel.DataAnnotations;

namespace HRMS.Api.Models
{
    public class RolePermissionMapping
    {
        [Key]
        public int RolePermissionMappingId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public int PermissionId { get; set; }

    }
}
