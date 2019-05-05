using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalService.Services
{
    public class HospitalStorageService : IHospitalStorageService
    {
        private readonly IDistributedCache _cache;
        public HospitalStorageService(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<Contracts.V2.Company> GetCompanyAsync(string guid, CancellationToken cancellationToken)
        {
            var data = await _cache.GetStringAsync(guid, cancellationToken);
            if (string.IsNullOrWhiteSpace(data))
                return new Contracts.V2.Company();
            else
                return JsonConvert.DeserializeObject<Contracts.V2.Company>(data);
        }

        public async Task<Contracts.V2.Hospital> GetHospitalAsync(string guid, CancellationToken cancellationToken)
        {
            var data = await _cache.GetStringAsync(guid, cancellationToken);
            if (string.IsNullOrWhiteSpace(data))
                return new Contracts.V2.Hospital();
            else
                return JsonConvert.DeserializeObject<Contracts.V2.Hospital>(data);
        }

        public async Task<Contracts.V2.Department> GetHospitalDepartmentAsync(string guid, CancellationToken cancellationToken)
        {
            var data = await _cache.GetStringAsync(guid, cancellationToken);
            if (string.IsNullOrWhiteSpace(data))
                return new Contracts.V2.Department();
            else
                return JsonConvert.DeserializeObject<Contracts.V2.Department>(data);
        }

        public async Task<Contracts.V2.Doctor> GetHospitalDepartmentDoctorAsync(string guid, CancellationToken cancellationToken)
        {
            var data = await _cache.GetStringAsync(guid, cancellationToken);
            if (string.IsNullOrWhiteSpace(data))
                return new Contracts.V2.Doctor();
            else
                return JsonConvert.DeserializeObject<Contracts.V2.Doctor>(data);
        }

        public async Task UpdateCompanyAsync(Contracts.V2.Company company, CancellationToken cancellationToken)
        {
            await _cache.SetStringAsync(company.GUID, JsonConvert.SerializeObject(company), cancellationToken);
            foreach (var hospital in company.Hospitals)
            {
                await _cache.SetStringAsync(hospital.GUID, JsonConvert.SerializeObject(hospital), cancellationToken);
                foreach (var department in hospital.Departments)
                {
                    await _cache.SetStringAsync(department.GUID, JsonConvert.SerializeObject(department), cancellationToken);
                    foreach (var doctor in department.Doctors)
                    {
                        await _cache.SetStringAsync(doctor.GUID, JsonConvert.SerializeObject(doctor), cancellationToken);
                    }
                }
            }
        }
    }
}
