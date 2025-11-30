namespace HRMS.Api.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = "";
        public string Iso2 { get; set; } = "";   // Example: IN, US, AU
        public string Iso3 { get; set; } = "";   // Example: IND, USA, AUS

        public ICollection<State> States { get; set; } = new List<State>();
    }
}
