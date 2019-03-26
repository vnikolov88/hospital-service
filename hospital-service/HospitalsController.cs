using hospital_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace hospital_service.Controllers
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
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            var hospitals = await MakeRequest<PagedSearch<Hospital>>(
                $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}", 
                "post",
                new[] { "0300", "0103" });

            return hospitals;
        }

        [HttpGet("aneurysm")]
        public async Task<ActionResult<PagedSearch<Hospital>>> GetForAneurysmAsync(
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            var hospitals = await MakeRequest<PagedSearch<Hospital>>(
                $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}",
                "post",
                new[] { "1800" });

            return hospitals;
        }

        [HttpGet("shock")]
        public async Task<ActionResult<PagedSearch<Hospital>>> GetForShockAsync(
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            var hospitals = await MakeRequest<PagedSearch<Hospital>>(
                $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}",
                "post",
                new[] { "0100" });

            return hospitals;
        }

        [HttpPost]
        public async Task<ActionResult<PagedSearch<Hospital>>> Post(
            [FromBody] string codes,
            int distance,
            string address,
            int page = 1,
            int pageSize = 20,
            bool sortByPatientCount = false)
        {
            // Get hospitales with this codes
            var hospitals = await MakeRequest<PagedSearch<Hospital>>(
                $"{_options.DoctorHelpRestUrl}/api/v1/hospitals/query?distance={distance}&address={address}&page={page}&pageSize={pageSize}&sortByPatientCount={sortByPatientCount}",
                "post",
                codes);

            return hospitals;
        }

        public async Task<TResultType> MakeRequest<TResultType>(string url, string requestType = "GET", object bodyData = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "*/*";
            request.UseDefaultCredentials = false;
            request.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";

            switch (requestType.ToLower())
            {
                case "put":
                    request.Method = "PUT";
                    break;
                case "delete":
                    request.Method = "DELETE";
                    break;
                case "post":
                    request.Method = "POST";
                    request.ContentType = "application/json";

                    using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
                    {
                        var json = JsonConvert.SerializeObject(bodyData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }
                    break;
                default:
                    request.Method = "GET";
                    break;
            }

            WebResponse response = await request.GetResponseAsync();
            var readResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var readResponseToJson = JsonConvert.DeserializeObject<TResultType>(readResponse,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Error = (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs errorArgs) =>
                    {
                        var currentError = errorArgs.ErrorContext.Error.Message;
                        errorArgs.ErrorContext.Handled = true;
                    }
                });

            return readResponseToJson;
        }
    }
}
