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
    public class FinanceInformationController : ControllerBase
    {
        private readonly IFinanceInformationService _notiSer;
        public FinanceInformationController(IFinanceInformationService notiSer) => _notiSer = notiSer;

        [HttpGet]
        [Route("portal/finance-Informations"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var financeInformations = await _notiSer.GetServiceAsync(paging);
            if (financeInformations.Data is null)
            {
                return NotFound(financeInformations);
            }
            var metadata = new
            {
                financeInformations.Data.TotalCount,
                financeInformations.Data.PageSize,
                financeInformations.Data.CurrentPage,
                financeInformations.Data.TotalPages,
                financeInformations.Data.HasNext,
                financeInformations.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(financeInformations);
        }

        [HttpGet]
        [Route("portal/finance-Informations/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var financeInformation = await _notiSer.GetByIdServiceAsync(id);
            if (financeInformation.Data is null)
            {
                return NotFound(financeInformation);
            }
            return Ok(financeInformation);
        }

        [HttpGet]
        [Route("portal/finance-Informations/{id:int}/student"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByStudentId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var financeInformation = await _notiSer.GetByStudentIdServiceAsync(id);
            if (financeInformation.Data is null)
            {
                return NotFound(financeInformation);
            }
            return Ok(financeInformation);
        }


        [HttpPost]
        [Route("portal/finance-Informations"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FinanceInformationDTO FinanceInformationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var financeInformation = await _notiSer.CreateServiceAsync(FinanceInformationDTO);
            if (financeInformation.Data is null)
            {
                return BadRequest(financeInformation);
            }
            return CreatedAtAction(nameof(Get), financeInformation);
        }

        [HttpPut]
        [Route("portal/finance-Informations/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] FinanceInformationDTO FinanceInformationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var financeInformation = await _notiSer.UpdateServiceAsync(id, FinanceInformationDTO);
            if (financeInformation.Data is null)
            {
                return NotFound(financeInformation);
            }
            return Ok(financeInformation);
        }

        [HttpDelete]
        [Route("portal/finance-Informations/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var financeInformation = await _notiSer.DeleteServiceAsync(id);
            if (financeInformation.Data is false)
            {
                return NotFound(financeInformation);
            }
            return NoContent();
        }
    }
}
