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
    public class FileDownloadController : ControllerBase
    {
        private readonly IFileDownloadService _fileSer;
        public FileDownloadController(IFileDownloadService fileSer) => _fileSer = fileSer;

        [HttpGet]
        [Route("portal/files"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var files = await _fileSer.GetServiceAsync(paging);
            if (files.Data is null)
            {
                return NotFound(files);
            }
            var metadata = new
            {
                files.Data.TotalCount,
                files.Data.PageSize,
                files.Data.CurrentPage,
                files.Data.TotalPages,
                files.Data.HasNext,
                files.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(files);
        }

        [HttpGet]
        [Route("portal/files/{identification}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdentification(string identification)
        {
            if (identification is null)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var fileDownload = await _fileSer.GetByIdentificationServiceAsync(identification);
            if (fileDownload.Data is null)
            {
                return NotFound(fileDownload);
            }
            return Ok(fileDownload);
        }
        [HttpGet]
        [Route("portal/files/module/{module}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByModule([FromQuery] PagingQueryParameters paging, string module)
        {
            if (module is null)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var fileDownload = await _fileSer.GetByModuleServiceAsync(paging, module);
            if (fileDownload.Data is null)
            {
                return NotFound(fileDownload);
            }
            return Ok(fileDownload);
        }

        [HttpPost]
        [Route("portal/files"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FileDownloadDTO fileDownloadDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fileDownload = await _fileSer.CreateServiceAsync(fileDownloadDTO);
            if (fileDownload.Data is null)
            {
                return BadRequest(fileDownload);
            }
            fileDownload.Data.file = null;
            return CreatedAtAction(nameof(Get), fileDownload);
        }

        [HttpPut]
        [Route("portal/files/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] FileDownloadDTO FileDownloadDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fileDownload = await _fileSer.UpdateServiceAsync(id, FileDownloadDTO);
            if (fileDownload.Data is null)
            {
                return NotFound(fileDownload);
            }
            fileDownload.Data.file = null;
            return Ok(fileDownload);
        }

        [HttpDelete]
        [Route("portal/files/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var fileDownload = await _fileSer.DeleteServiceAsync(id);
            if (fileDownload.Data is false)
            {
                return NotFound(fileDownload);
            }
            return NoContent();
        }
    }
}
