using Newtonsoft.Json;

namespace HospitalService.Contracts.V2
{
    public class Doctor
    {
        public string GUID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Salutation { get; set; }

        public Address Address { get; set; }

        public string Specialty { get; set; }

        public Picture[] Pictures { get; set; }

        public string CVUrl { get; set; }

        [JsonIgnore]
        public double DistanceFromLocationKm { get; set; }
    }
}
