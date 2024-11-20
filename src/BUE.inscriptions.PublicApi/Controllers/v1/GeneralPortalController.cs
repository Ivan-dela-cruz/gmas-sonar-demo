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
    public class GeneralPortalController : ControllerBase
    {
        private readonly IGeneralPortalService _generalSer;
        public GeneralPortalController(IGeneralPortalService generalSer) => _generalSer = generalSer;

        [HttpGet("portal/status-list"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLevels([FromQuery] PagingQueryParameters paging)
        {
            var statusList = await _generalSer.getStatusAsync(paging);
            if (statusList.Data is null)
            {
                return NotFound(statusList);
            }
            var metadata = new
            {
                statusList.Data.TotalCount,
                statusList.Data.PageSize,
                statusList.Data.CurrentPage,
                statusList.Data.TotalPages,
                statusList.Data.HasNext,
                statusList.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(statusList);
        }

        [HttpGet("portal/school-years")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSchoolYears([FromQuery] PagingQueryParameters paging)
        {
            var schoolYears = await _generalSer.getSchoolYearsAsync(paging);
            if (schoolYears.Data is null)
            {
                return NotFound(schoolYears);
            }
            var metadata = new
            {
                schoolYears.Data.TotalCount,
                schoolYears.Data.PageSize,
                schoolYears.Data.CurrentPage,
                schoolYears.Data.TotalPages,
                schoolYears.Data.HasNext,
                schoolYears.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(schoolYears);
        }
        [HttpGet("portal/banks"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBanks([FromQuery] PagingQueryParameters paging)
        {
            var result = await _generalSer.getBankAsync(paging);
            if (result.Data is null)
            {
                return NotFound(result);
            }
            var metadata = new
            {
                result.Data.TotalCount,
                result.Data.PageSize,
                result.Data.CurrentPage,
                result.Data.TotalPages,
                result.Data.HasNext,
                result.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(result);
        }
        [HttpGet("portal/payment-methods"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaymentMethodAsync([FromQuery] PagingQueryParameters paging)
        {
            var result = await _generalSer.getPaymentMethodAsync(paging);
            if (result.Data is null)
            {
                return NotFound(result);
            }
            var metadata = new
            {
                result.Data.TotalCount,
                result.Data.PageSize,
                result.Data.CurrentPage,
                result.Data.TotalPages,
                result.Data.HasNext,
                result.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(result);
        }
        [HttpGet("portal/debit-types"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDebitTypeAsync([FromQuery] PagingQueryParameters paging)
        {
            var result = await _generalSer.getDebitTypeAsync(paging);
            if (result.Data is null)
            {
                return NotFound(result);
            }
            var metadata = new
            {
                result.Data.TotalCount,
                result.Data.PageSize,
                result.Data.CurrentPage,
                result.Data.TotalPages,
                result.Data.HasNext,
                result.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(result);
        }
        [HttpGet("portal/relations-ship"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRelationsShip([FromQuery] PagingQueryParameters paging)
        {
            var relationsShip = await _generalSer.getRelationShipsAsync(paging);
            if (relationsShip.Data is null)
            {
                return NotFound(relationsShip);
            }
            var metadata = new
            {
                relationsShip.Data.TotalCount,
                relationsShip.Data.PageSize,
                relationsShip.Data.CurrentPage,
                relationsShip.Data.TotalPages,
                relationsShip.Data.HasNext,
                relationsShip.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(relationsShip);
        }
        [HttpGet("portal/credit-cards"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCreditCard([FromQuery] PagingQueryParameters paging)
        {
            var creditCard = await _generalSer.getCreditCardsAsync(paging);
            if (creditCard.Data is null)
            {
                return NotFound(creditCard);
            }
            var metadata = new
            {
                creditCard.Data.TotalCount,
                creditCard.Data.PageSize,
                creditCard.Data.CurrentPage,
                creditCard.Data.TotalPages,
                creditCard.Data.HasNext,
                creditCard.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(creditCard);
        }
        [HttpGet("portal/civil-status"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCivilStatus([FromQuery] PagingQueryParameters paging)
        {
            var result = await _generalSer.getCivilStatusAsync(paging);
            if (result.Data is null)
            {
                return NotFound(result);
            }
            var metadata = new
            {
                result.Data.TotalCount,
                result.Data.PageSize,
                result.Data.CurrentPage,
                result.Data.TotalPages,
                result.Data.HasNext,
                result.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(result);
        }
        [HttpGet("portal/report-record-student"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecordStudentReport([FromQuery] PagingQueryParameters paging)
        {
            var result = await _generalSer.getRecordStudentReportAsync(paging);
            if (result.Data is null)
            {
                return NotFound(result);
            }
            var metadata = new
            {
                result.Data.TotalCount,
                result.Data.PageSize,
                result.Data.CurrentPage,
                result.Data.TotalPages,
                result.Data.HasNext,
                result.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(result);
        }

    }
}
