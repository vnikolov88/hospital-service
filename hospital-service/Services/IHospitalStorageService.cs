using System.Threading;
using System.Threading.Tasks;

namespace HospitalService.Services
{
    public interface IHospitalStorageService
    {
        Task UpdateCompanyAsync(Contracts.V2.Company company, CancellationToken cancellationToken);
        Task<Contracts.V2.Company> GetCompanyAsync(string guid, CancellationToken cancellationToken);
        Task<Contracts.V2.Hospital> GetHospitalAsync(string guid, CancellationToken cancellationToken);
        Task<Contracts.V2.Department> GetHospitalDepartmentAsync(string guid, CancellationToken cancellationToken);
        Task<Contracts.V2.Doctor> GetHospitalDepartmentDoctorAsync(string guid, CancellationToken cancellationToken);
    }
}
