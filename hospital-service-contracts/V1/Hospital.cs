using System.Collections.Generic;

namespace HospitalService.Contracts.V1
{
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
