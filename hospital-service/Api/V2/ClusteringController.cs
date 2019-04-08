using Flurl.Http;
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
    public class ClusteringController : ControllerBase
    {
        private readonly StartupOptions _options;
        public ClusteringController(IOptions<StartupOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        [HttpGet("cannary")]
        public IActionResult Cannary() => Ok();

        [HttpGet]
        public async Task<IEnumerable<string>> Get(
            CancellationToken cancellationToken,
            [FromQuery]string param1,
            [FromQuery]string param2,
            [FromQuery]bool extended)
        {
            var expression = $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/params";
            if (!string.IsNullOrWhiteSpace(param1))
            {
                expression += $"?param1={param1}";
                if (!string.IsNullOrWhiteSpace(param2))
                {
                    expression += $"&param2={param2}";
                    if (extended)
                        expression += "&extended=extended";
                }
            }
            // Get codes based on clustering
            var codes = await expression
                .GetJsonAsync<IEnumerable<string>>(cancellationToken);

            return codes;
        }
    }
}
