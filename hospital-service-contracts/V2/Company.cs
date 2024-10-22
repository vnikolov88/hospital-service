﻿namespace HospitalService.Contracts.V2
{
    public class Company
    {
        public string GUID { get; set; }

        public string Name { get; set; }

        public string DescriptionHtml { get; set; }

        public Address Address { get; set; }

        public string SortOrder { get; set; }

        public Picture[] Pictures { get; set; }

        public Hospital[] Hospitals { get; set; }

        public Doctor[] Personal { get; set; }

        public Certificate[] Certificates { get; set; }
    }
}
