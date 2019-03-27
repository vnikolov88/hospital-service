using Flurl.Http;
using hospital_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hospital_service.Api.V1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class HospitalsController : ControllerBase
    {
        private readonly StartupOptions _options;
        public HospitalsController(IOptions<StartupOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        [HttpGet("heart-attack")]
        public async Task<ActionResult<PagedSearch<Hospital>>> GetForHeartAttackAsync(
            CancellationToken cancellationToken,
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            var hospitals = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}"
                .PostJsonAsync( new[] { "0300", "0103" }, cancellationToken)
                .ReceiveJson<PagedSearch<Hospital>>();

            return hospitals;
        }

        [HttpGet("aneurysm")]
        public async Task<ActionResult<PagedSearch<Hospital>>> GetForAneurysmAsync(
            CancellationToken cancellationToken,
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            var hospitals = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}"
                .PostJsonAsync(new[] { "1800" }, cancellationToken)
                .ReceiveJson<PagedSearch<Hospital>>();

            return hospitals;
        }

        [HttpGet("shock")]
        public async Task<ActionResult<PagedSearch<Hospital>>> GetForShockAsync(
            CancellationToken cancellationToken,
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            var hospitals = await $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}"
                .PostJsonAsync(new[] { "0100" }, cancellationToken)
                .ReceiveJson<PagedSearch<Hospital>>();

            return hospitals;
        }

        [HttpPost]
        public async Task<ActionResult<PagedSearch<Hospital>>> Post(
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
                .ReceiveJson<PagedSearch<Hospital>>();

            return hospitals;
        }
    }
}
