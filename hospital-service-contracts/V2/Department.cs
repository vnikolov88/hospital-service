using Newtonsoft.Json;

namespace HospitalService.Contracts.V2
{
    public class Department
    {
        public string GUID { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public string SortOrder { get; set; }

        public Picture[] Pictures { get; set; }

        public string WorktimeMessageHtml { get; set; }

        public string DescriptionHtml { get; set; }

        public Certificate[] Certificates { get; set; }

        public string DepartmentClassification { get; set; }

        public Doctor[] Doctors { get; set; }

        [JsonIgnore]
        public double DistanceFromLocationKm { get; set; }
    }
}
