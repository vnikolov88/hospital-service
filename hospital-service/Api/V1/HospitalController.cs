using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalService.Api.V1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class HospitalController : ControllerBase
    {
        private readonly StartupOptions _options;
        public HospitalController(IOptions<StartupOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        [HttpGet("cannary")]
        public IActionResult Cannary() => Ok();

        [HttpGet("heart-attack")]
        public async Task<ActionResult<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>> GetForHeartAttackAsync(
            CancellationToken cancellationToken,
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            var hospitals = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}"
                .PostJsonAsync( new[] { "0300", "0103" }, cancellationToken)
                .ReceiveJson<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>();

            return hospitals;
        }

        [HttpGet("aneurysm")]
        public async Task<ActionResult<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>> GetForAneurysmAsync(
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

            return hospitals;
        }

        [HttpGet("shock")]
        public async Task<ActionResult<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>> GetForShockAsync(
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

            return hospitals;
        }

        [HttpPost]
        public async Task<ActionResult<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>> Post(
            CancellationToken cancellationToken,
            [FromBody] string codes,
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            // Get hospitales with this codes
            var hospitals = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}"
                .PostJsonAsync(codes, cancellationToken)
                .ReceiveJson<Contracts.V1.PagedSearch<Contracts.V1.Hospital>>();

            return hospitals;
        }
    }
}
