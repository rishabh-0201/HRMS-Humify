using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Api.Models
{
    
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public string MenuUrl { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int? ParentMenuId { get; set; }
        public int OrderNo { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
