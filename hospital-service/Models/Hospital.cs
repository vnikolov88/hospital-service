using System.Collections.Generic;

namespace hospital_service.Models
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

    public class ContactPerson
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MainAreaOfFocus { get; set; }
        public string PhonePrefix { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxPrefix { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
    }
    public class MedicalService
    {
        public string Code { get; set; }
        public string Designation { get; set; }
        public string Explanations { get; set; }
    }
    public class MedicalEquipment
    {
        public string Code { get; set; }
        public string Notfallverfuegbarkeit_24h { get; set; }
        public string Explanations { get; set; }
    }

    public class Hospital
    {
        public string Name { get; set; }
        public string IK { get; set; }
        public Address Address { get; set; }
        public List<Department> Departments { get; set; }
        public List<MedicalEquipment> MedicalEquipments { get; set; }
        public double DistanceFromLocation { get; set; }

        public Department CurrentDepartment { get; set; }
    }
}
