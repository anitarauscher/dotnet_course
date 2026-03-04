namespace Epam.Automation.src.Api.Models
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public AddressDto? Address { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public CompanyDto? Company { get; set; }
    }

    public class AddressDto
    {
        public string? Street { get; set; }
        public string? Suite { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
        public GeoDto? Geo { get; set; }
    }

    public class GeoDto
    {
        public string? Lat { get; set; }
        public string? Lng { get; set; }
    }

    public class CompanyDto
    {
        public string? Name { get; set; }
        public string? CatchPhrase { get; set; }
        public string? Bs { get; set; }
    }
}