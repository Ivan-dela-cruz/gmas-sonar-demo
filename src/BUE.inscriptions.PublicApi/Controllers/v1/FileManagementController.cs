using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Asp.Versioning;

namespace BUE.Inscriptions.PublicApi.Controllers.v1
{

    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class FileManagementController : ControllerBase
    {
        private readonly IManagementFileCloudService _fileSer;
        public FileManagementController(IManagementFileCloudService fileSer) => _fileSer = fileSer;

        [HttpGet]
        [Route("portal/files-management"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByFileFromCloudRepository([FromQuery] string objectKey)
        {
            if (objectKey is null)
            {
                return BadRequest(MessageUtil.Instance.Empty);
            }
            var fileDownload = await _fileSer.GetFileBytesFromCloudAsync(objectKey);
            if (fileDownload.Data is null)
            {
                return NotFound(fileDownload);
            }
            return Ok(fileDownload);
        }
    }
}
