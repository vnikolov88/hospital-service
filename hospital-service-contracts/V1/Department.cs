using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Contracts.V1
{
    public class Department
    {
        public string Name { get; set; }
        public List<string> ClassificationNumbers { get; set; }
        public bool ProceduresAvailable { get; set; }
        public bool DiagnosesAvailable { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public Dictionary<string, string> Procedures { get; set; }
        public Dictionary<string, string> Diagnoses { get; set; }
        public double TotalDiagnosesPatients { get; set; }
        public double TotalProceduresPatients { get; set; }
        public Address Address { get; set; }
        public List<MedicalService> MedicalServices { get; set; }

        public int MakeKey { get; set; }
        public int ChiefMakeKey { get; set; }

        // TLHOW DATA
        public TLHOWData TLHOW { get; set; }
    }
}
