using AutoMapper;
using Flurl.Http;
using HospitalService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalService.Api.V2
{
    [Produces("application/json")]
    [Route("api/v2/[controller]")]
    public class HospitalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly StartupOptions _options;
        private readonly IHospitalStorageService _hospitalStorageService;

        public HospitalController(
            IMapper mapper,
            IOptions<StartupOptions> options,
            IHospitalStorageService hospitalStorageService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _hospitalStorageService = hospitalStorageService ?? throw new ArgumentNullException(nameof(hospitalStorageService));
        }

        [HttpGet("cannary")]
        public IActionResult Cannary() => Ok();

        [HttpGet("heart-attack")]
        public async Task<ActionResult<Contracts.V1.PagedSearch<Contracts.V2.Hospital>>> GetForHeartAttackAsync(
           CancellationToken cancellationToken,
           int distance,
           string address,
           int page = 1,
           int pageSize = 20,
           bool sortByPatientCount = false)
        {
            var hospitals = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}"
                .PostJsonAsync(new[] { "0300", "0103" }, cancellationToken)
                .ReceiveJson<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>();

            return _mapper.Map<Contracts.V1.PagedSearch<Contracts.V2.Hospital>>(hospitals);
        }

        [HttpGet("aneurysm")]
        public async Task<ActionResult<Contracts.V1.PagedSearch<Contracts.V2.Hospital>>> GetForAneurysmAsync(
            CancellationToken cancellationToken,
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            var hospitals = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}"
                .PostJsonAsync(new[] { "1800" }, cancellationToken)
                .ReceiveJson<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>();

            return _mapper.Map<Contracts.V1.PagedSearch<Contracts.V2.Hospital>>(hospitals);
        }

        [HttpGet("shock")]
        public async Task<ActionResult<Contracts.V1.PagedSearch<Contracts.V2.Hospital>>> GetForShockAsync(
            CancellationToken cancellationToken,
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            var hospitals = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}"
                .PostJsonAsync(new[] { "0100" }, cancellationToken)
                .ReceiveJson<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>();

            return _mapper.Map<Contracts.V1.PagedSearch<Contracts.V2.Hospital>>(hospitals);
        }

        [HttpGet]
        public async Task<ActionResult<Contracts.V1.PagedSearch<Contracts.V2.Hospital>>> Get(
            CancellationToken cancellationToken,
            string param1,
            string param2,
            string param3,
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            // Get codes based on clustering
            var codes = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/codes?param1={param1}&param2={param2}&param3={param3}"
                .GetJsonAsync<IEnumerable<string>>(cancellationToken);

            // Get hospitales with this codes
            var hospitals = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}"
                .PostJsonAsync(codes, cancellationToken)
                .ReceiveJson<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>();

            return _mapper.Map<Contracts.V1.PagedSearch<Contracts.V2.Hospital>>(hospitals);
        }

        [HttpGet("get")]
        public async Task<ActionResult<Contracts.V2.Hospital>> GetByGuid(CancellationToken cancellationToken, string guid)
        {
            return await _hospitalStorageService.GetHospitalAsync(guid, cancellationToken);
        }

        [HttpGet("get-department")]
        public async Task<ActionResult<Contracts.V2.Department>> GetDepartmentByGuid(CancellationToken cancellationToken, string guid)
        {
            return await _hospitalStorageService.GetHospitalDepartmentAsync(guid, cancellationToken);
        }

        [HttpGet("get-department-doctor")]
        public async Task<ActionResult<Contracts.V2.Doctor>> GetDepartmentDoctorByGuid(CancellationToken cancellationToken, string guid)
        {
            return await _hospitalStorageService.GetHospitalDepartmentDoctorAsync(guid, cancellationToken);
        }

        [HttpPost("update-company")]
        public async Task<IActionResult> UpdateCompany(
            CancellationToken cancellationToken,
            Contracts.V2.Company company)
        {
            await _hospitalStorageService.UpdateCompanyAsync(company, cancellationToken);
            return Ok();
        }

        [HttpGet("get-company")]
        public async Task<ActionResult<Contracts.V2.Company>> GetCompanyByGuid(CancellationToken cancellationToken, string guid)
        {
            return await _hospitalStorageService.GetCompanyAsync(guid, cancellationToken);
        }
    }
}
