using Newtonsoft.Json;

namespace HospitalService.Contracts.V2
{
    public class Hospital
    {
        public string GUID { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public string SortOrder { get; set; }

        public Picture[] Pictures { get; set; }

        public Department[] Departments { get; set; }

        [JsonIgnore]
        public double DistanceFromLocationKm { get; set; }
    }
}
