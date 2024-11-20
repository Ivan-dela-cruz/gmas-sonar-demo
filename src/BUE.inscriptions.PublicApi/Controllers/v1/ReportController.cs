using BUE.Inscriptions.Domain.Entity.DTO.storeProcedures;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.EviCertia;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Domain.Reports;
using Newtonsoft.Json;
using System.Text;
using Azure;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using Newtonsoft.Json.Serialization;
using Asp.Versioning;

namespace BUE.Inscriptions.PublicApi.Controllers.v1
{

    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportSer;
        private readonly IOrganizationVoteService _organizationVoteService;
        private readonly IPortalRequestService _requestSer;
        private readonly IAwsS3UploaderRepository _awsS3;
        protected readonly IConfiguration _configuration;
        public ReportController(IOrganizationVoteService organizationVoteService, IReportService reportSer, IAwsS3UploaderRepository awsS3, IPortalRequestService requestSer, IConfiguration configuration)
        {
            _reportSer = reportSer;
            _awsS3 = awsS3;
            _requestSer = requestSer;
            _configuration = configuration;
            _organizationVoteService = organizationVoteService;
        }
        [HttpPost]
        [Route("portal/reports"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReportGerate([FromBody] ReportParameterDTO parameters)
        {
            if (parameters.reportType is null || parameters.reportType == "" || parameters.reportName is null || parameters.reportName == "")
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            if (parameters.reportType.Trim().ToUpper() != "PDF" && parameters.reportType.Trim().ToUpper() != "XLSX")
            {
                return BadRequest(MessageUtil.Instance.NotFound + " tipo reporte " + parameters.reportType);
            }
            var report = await _reportSer.GetReportPDFServiceAsync(parameters);
            if (report.Data is null)
            {
                return NotFound(report);
            }
            return Ok(report);
        }
        [HttpPost]
        [Route("portal/contracts/files"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPdfContract([FromBody] ReportParameterDTO parameters)
        {
            IBaseResponse<ReportParameterDTO> report = await _reportSer.GenerateContractServiceAsync(parameters);
            IActionResult pdfContract = report != null ? report.Data != null ? Ok(report) : NotFound(report) : NotFound(report);
            report = null;
            return pdfContract;
        }

        [HttpPost]
        [Route("portal/contracts/pdf"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreatePdfContract([FromBody] ReportParameterDTO parameters)
        {
            IBaseResponse<ReportParameterDTO> report = await _reportSer.GenerateTaskContractServiceAsync(parameters);
            IActionResult pdfContract = report != null ? report.Data != null ? Ok(report) : NotFound(report) : NotFound(report);
            report = null;
            return pdfContract;
        }

        [HttpPost]
        [Route("portal/contracts/pdf2"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreatePdfContract2([FromBody] ReportParameterDTO parameters)
        {
            IBaseResponse<ReportParameterDTO> report = await _reportSer.GetReportPDF2ServiceAsync(parameters);
            IActionResult pdfContract2 = report != null ? report.Data != null ? Ok(report) : NotFound(report) : NotFound(report);
            report = null;
            return pdfContract2;
        }

        [HttpPost]
        [Route("portal/contracts/send"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ContractReportGerate([FromBody] SingFileDTO parameters)
        {
            IBaseResponse<string> report = await _reportSer.SendServiceContractServiceAsync(parameters);
            IActionResult actionResult = report.status ? Ok(report) : NotFound(report);
            report = null;
            return actionResult;
        }

        [HttpGet]
        [Route("portal/contracts/evicertia"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetQuuerySingFile([FromQuery] string id, bool includeDocument)
        {
            IBaseResponse<EviQuerySingDTO> report = await _reportSer.GetDocumentFromEviCertiaAsync(id, includeDocument);
            IActionResult quuerySingFile = report.status ? Ok(report) : NotFound(report);
            report = null;
            return quuerySingFile;
        }

        [HttpPost]
        [Route("portal/contracts/bank"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> BankContractReportGerate([FromBody] SingFileDTO parameters)
        {
            IBaseResponse<SingFileDTO> report = await _reportSer.GenerateBankContractServiceAsync(parameters);
            IActionResult actionResult = report != null ? Ok(report) : NotFound(report);
            report = null;
            return actionResult;
        }

        [HttpPost]
        [Route("portal/contracts/data"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DataContractReportGerate([FromBody] SingFileDTO parameters)
        {
            IBaseResponse<SingFileDTO> report = await _reportSer.GenerateDataContractServiceAsync(parameters);
            IActionResult actionResult = report != null ? Ok(report) : NotFound(report);
            report = null;
            return actionResult;
        }
        [HttpPost]
        [Route("portal/contracts/records"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> generateReports([FromBody] RecordStudentReport records)
        {
            try
            {
                var client = new HttpClient();
                var requestUri = _configuration.GetSection("AppSettings:ExternalReportServer").Value + "/api/student/report";
                var jsonPayload = JsonConvert.SerializeObject(records);
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
                await _requestSer.UpdateUrlReportCompleteAsync((int)records.codigoMatricula, apiResponse.Data.UrlReport);
                return Ok(apiResponse.Data.UrlReport);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("portal/report/dhont-elections/{ElectionId}"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GenerateDHontReport(int ElectionId)
        {
            try
            {
                var requestUri = _configuration.GetSection("AppSettings:ExternalReportServer").Value + "/api/report-templates/7/generate";

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                };

                var client = new HttpClient();
                var dhontResult = await _organizationVoteService.GetDHontResultOrganizationElectionAsync(ElectionId);
                var jsonPayload = JsonConvert.SerializeObject(dhontResult.Data, settings);

                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

    }

    class ReportData
    {
        public string UrlReport { get; set; }
    }

    class ApiResponse
    {
        public ReportData Data { get; set; }
    }

}
