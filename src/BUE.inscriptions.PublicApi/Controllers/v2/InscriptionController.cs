using Asp.Versioning;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BUE.Inscriptions.PublicApi.Controllers.v2
{
    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("2.0")]
    [ApiController]
    [Produces("application/json")]
    public class InscriptionController : ControllerBase
    {
        private readonly IInscriptionService _inscriptionService;

        public InscriptionController(IInscriptionService inscriptionService)
        {
            _inscriptionService = inscriptionService;
        }

        [HttpGet]
        [Route("inscriptions"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var inscription = await _inscriptionService.GetByAcademicYearAsync(paging);
            if (inscription.Data is null)
            {
                return NotFound(inscription);
            }

            var metadata = new
            {
                inscription.Data.TotalCount,
                inscription.Data.PageSize,
                inscription.Data.CurrentPage,
                inscription.Data.TotalPages,
                inscription.Data.HasNext,
                inscription.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(inscription);
        }

        [HttpGet]
        [Route("inscriptions/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var inscription = await _inscriptionService.GetByIdServiceAsync(id);
            if (inscription.Data is null)
            {
                return NotFound(inscription);
            }

            return Ok(inscription);
        }

        [HttpPost]
        [Route("inscriptions"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] InscriptionDTO inscriptionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscription = await _inscriptionService.CreateServiceAsync(inscriptionDTO);
            if (inscription.Data is null)
            {
                return BadRequest(inscription);
            }

            return CreatedAtAction(nameof(Get), inscription);
        }

        [HttpPut]
        [Route("inscriptions/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] InscriptionDTO inscriptionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscription = await _inscriptionService.UpdateServiceAsync(id, inscriptionDTO);
            if (inscription.Data is null)
            {
                return NotFound(inscription);
            }

            return Ok(inscription);
        }

        [HttpPut]
        [Route("inscriptions/{id}/process"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProcess(int id, [FromBody] InscriptionDTO inscriptionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TraceLog(inscriptionDTO, "REQUEST");
            var inscription = await _inscriptionService.UpdateProcessServiceAsync(id, inscriptionDTO);
            if (inscription.Data is null)
            {
                return NotFound(inscription);
            }
            TraceLog(inscription.Data, "RESPONSE");
            return Ok(inscription);
        }
        [HttpPut]
        [Route("inscriptions/status"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus([FromBody] RequestStatusChange requestStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TraceLogStatus(requestStatus, "REQUEST");
            var inscription = await _inscriptionService.UpdateStatusServiceAsync(requestStatus);
            if (inscription.Data == false)
            {
                return BadRequest(inscription);
            }

            return Ok(inscription);
        }

        [HttpDelete]
        [Route("inscriptions/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var inscription = await _inscriptionService.DeleteServiceAsync(id);
            if (inscription.Data is false)
            {
                return NotFound(inscription);
            }

            return NoContent();
        }

        public void TraceLog(InscriptionDTO inscriptionDTO, string action = "REQUEST")
        {
            try
            {
                var jsonItem = JsonConvert.SerializeObject(inscriptionDTO);
                LogManagement.Instance.write("INSCRIPTIONS", action, $"[{inscriptionDTO.Id}] => [ " + jsonItem + " ]", "BUE.Inscriptions.PublicApi.Controllers.v2.INSCRIPTIONS");
            }
            catch (Exception e)
            {
            }
        }
        public void TraceLogStatus(RequestStatusChange requestStatus, string action = "REQUEST")
        {
            try
            {
                var jsonItem = JsonConvert.SerializeObject(requestStatus);
                LogManagement.Instance.write("INSCRIPTIONS", action, $"[{requestStatus.Ids}] => [ " + jsonItem + " ]", "BUE.Inscriptions.PublicApi.Controllers.v2.INSCRIPTIONS");
            }
            catch (Exception e)
            {
            }
        }
    }
}
