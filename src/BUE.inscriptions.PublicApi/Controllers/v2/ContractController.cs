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

namespace BUE.Inscriptions.PublicApi.Controllers.v2
{

    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IStudentService _studentService;
        protected readonly IConfiguration _configuration;
        public ContractController(IContractService contractService, IStudentService studentService)
        {
            _contractService = contractService;
            _studentService = studentService;
        }

        [HttpPost]
        [Route("contracts/evicertia"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ContractReportGerate([FromBody] SingFileDTO parameters)
        {
            IBaseResponse<string> report = await _contractService.SendServiceContractServiceAsync(parameters);
            try
            {
                var jsonItem = JsonConvert.SerializeObject(parameters);
                LogManagement.Instance.write("EVICERTA", "ContractReportGerate", $"[{parameters.requestCode}] => [ " + jsonItem + " ]", "BUE.Inscriptions.Controlle.Evicertia");
            }
            catch (Exception) { }
            IActionResult actionResult = report.status ? Ok(report) : NotFound(report);
            return actionResult;
        }
        [HttpPost]
        [Route("contracts/evicertia/cancel"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CancelContractReportGerate([FromBody] EvicertiaCancelDTO parameters)
        {
            IBaseResponse<string> report = await _contractService.CancelContractAsync(parameters);
    
            IActionResult actionResult = report.status ? Ok(report) : NotFound(report);
            return actionResult;
        }

        [HttpPost]
        [Route("contracts/generate"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ContractGenerateReport([FromBody] TemplateReport templateReport)
        {
            var report = await _contractService.ContractGenerateServiceAsync(templateReport);
            IActionResult actionResult = report.status ? Ok(report) : NotFound(report);
            return actionResult;

        }
       
    }


}
