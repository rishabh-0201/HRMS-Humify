using System.ComponentModel.DataAnnotations;

namespace HRMS.Api.Models
{
    public class RoleMenuMapping
    {
        [Key]
        public int RoleMenuId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get;set; }

    }
}
