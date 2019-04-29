namespace HospitalService.Contracts.V2
{
    public class Address
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public string Street { get; set; }

        public string StreetNr { get; set; }

        public string Url { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
