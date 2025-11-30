namespace HRMS.Api.Models
{
    public class Department
    {
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
    }
}
