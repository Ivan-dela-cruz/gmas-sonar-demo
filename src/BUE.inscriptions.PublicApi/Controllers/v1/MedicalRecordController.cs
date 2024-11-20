using Asp.Versioning;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BUE.Inscriptions.PublicApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/portal/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet]
        [Route("medical-records"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var medicalRecords = await _medicalRecordService.GetServiceAsync(paging);
            if (medicalRecords.Data is null)
            {
                return NotFound(medicalRecords);
            }

            return Ok(medicalRecords);
        }

        [HttpGet]
        [Route("medical-records/{id}"), Authorize]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var medicalRecord = await _medicalRecordService.GetByIdServiceAsync(id);
            if (medicalRecord.Data is null)
            {
                return NotFound(medicalRecord);
            }

            return Ok(medicalRecord);
        }

        [HttpPost]
        [Route("medical-records"), Authorize]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MedicalRecordDTO medicalRecordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicalRecord = await _medicalRecordService.CreateServiceAsync(medicalRecordDTO);
            if (medicalRecord.Data is null)
            {
                return BadRequest(medicalRecord);
            }

            return CreatedAtAction(nameof(Get), medicalRecord);
        }

        [HttpPut]
        [Route("medical-records/{id}"), Authorize]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] MedicalRecordDTO medicalRecordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicalRecord = await _medicalRecordService.UpdateServiceAsync(id, medicalRecordDTO);
            if (medicalRecord.Data is null)
            {
                return NotFound(medicalRecord);
            }

            return Ok(medicalRecord);
        }

        [HttpDelete]
        [Route("medical-records/{id}"), Authorize]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var medicalRecord = await _medicalRecordService.DeleteServiceAsync(id);
            if (medicalRecord.Data is false)
            {
                return NotFound(medicalRecord);
            }

            return NoContent();
        }
    }
}
