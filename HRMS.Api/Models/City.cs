namespace HRMS.Api.Models
{
    public class City
    {
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public string CityName { get; set; } = "";
        public Country Country { get; set; }
        public State? State { get; set; }
    }
}
