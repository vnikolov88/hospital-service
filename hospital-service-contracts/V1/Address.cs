namespace HospitalService.Contracts.V1
{
    public class Address
    {
        public string Street { get; set; }
        public string StreetNr { get; set; }
        public string Postcode { get; set; }
        public string Place { get; set; }
        public string Url { get; set; }
        // Added so the deserialization can pick them up
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
