using BUE.Inscriptions.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Response;
using Asp.Versioning;
using Newtonsoft.Json;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Shared.Utils;
using BUE.Inscriptions.Domain.Entity.DTO.EviCertia;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BUE.Inscriptions.PublicApi.Controllers.v2
{

    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("2.0")]
    [ApiController]
    [Produces("application/json")]
    public class ReportController : ControllerBase
    {
        private readonly IExcelReportService _excelReport;
        protected readonly IConfiguration _configuration;
        public ReportController(IExcelReportService excelReport)
        {
            _excelReport = excelReport;
        }
        [HttpPost]
        [Route("excel/transport"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ExcelGenerateReport([FromBody] TemplateReport templateReport)
        {
            var report = await _excelReport.TransportExcelGenerateServiceAsync(templateReport);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "reporte_transport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            if (report.status != true)
                return NotFound(report);
            return File(report.Data, contentType, fileName);
        }
        [HttpPost]
        [Route("excel/autorizations"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AutorizationsExcelGenerateReport([FromBody] TemplateReport templateReport)
        {
            var report = await _excelReport.AutorizationsExcelGenerateServiceAsync(templateReport);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "reporte_autorizations_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            if (report.status != true)
                return NotFound(report);
            return File(report.Data, contentType, fileName);
        }
    }


}
