using System.Text.Json.Serialization;

namespace HRMS.Api.Models
{
    public class Company
    {

        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyCode {  get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<User>? Users { get; set; }
    }
}
