using Flurl.Http;
using hospital_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace hospital_service.Api.V2
{
    [Produces("application/json")]
    [Route("api/v2/[controller]")]
    public class HospitalController : ControllerBase
    {
        private readonly StartupOptions _options;
        public HospitalController(IOptions<StartupOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        [HttpGet("cannary")]
        public IActionResult Cannary() => Ok();

        [HttpGet]
        public async Task<ActionResult<PagedSearch<Hospital>>> Get(
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
                .ReceiveJson<PagedSearch<Hospital>>();

            return hospitals;
        }
    }
}
