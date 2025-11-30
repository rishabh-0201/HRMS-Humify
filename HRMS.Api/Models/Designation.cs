namespace HRMS.Api.Models
{
    public class Designation
    {
        public int CompanyId { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
    }
}
