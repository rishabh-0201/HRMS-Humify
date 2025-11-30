namespace HRMS.Api.Models
{
    public class State
    {
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string StateName { get; set; } = "";
        public string? StateCode { get; set; }

        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
