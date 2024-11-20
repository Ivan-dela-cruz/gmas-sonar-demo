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
    public class PaymentInformationController : ControllerBase
    {
        private readonly IPaymentInformationService _paymentInformationService;

        public PaymentInformationController(IPaymentInformationService paymentInformationService)
        {
            _paymentInformationService = paymentInformationService;
        }

        [HttpGet]
        [Route("payment-information"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var paymentInformation = await _paymentInformationService.GetServiceAsync(paging);
            if (paymentInformation.Data is null)
            {
                return NotFound(paymentInformation);
            }

            var metadata = new
            {
                paymentInformation.Data.TotalCount,
                paymentInformation.Data.PageSize,
                paymentInformation.Data.CurrentPage,
                paymentInformation.Data.TotalPages,
                paymentInformation.Data.HasNext,
                paymentInformation.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(paymentInformation);
        }

        [HttpGet]
        [Route("payment-information/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var paymentInformation = await _paymentInformationService.GetByIdServiceAsync(id);
            if (paymentInformation.Data is null)
            {
                return NotFound(paymentInformation);
            }

            return Ok(paymentInformation);
        }

        [HttpPost]
        [Route("payment-information"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PaymentInformationDTO paymentInformationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentInformation = await _paymentInformationService.CreateServiceAsync(paymentInformationDTO);
            if (paymentInformation.Data is null)
            {
                return BadRequest(paymentInformation);
            }

            return CreatedAtAction(nameof(Get), paymentInformation);
        }

        [HttpPut]
        [Route("payment-information/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] PaymentInformationDTO paymentInformationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentInformation = await _paymentInformationService.UpdateServiceAsync(id, paymentInformationDTO);
            if (paymentInformation.Data is null)
            {
                return NotFound(paymentInformation);
            }

            return Ok(paymentInformation);
        }

        [HttpDelete]
        [Route("payment-information/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var paymentInformation = await _paymentInformationService.DeleteServiceAsync(id);
            if (paymentInformation.Data is false)
            {
                return NotFound(paymentInformation);
            }

            return NoContent();
        }
    }
}
