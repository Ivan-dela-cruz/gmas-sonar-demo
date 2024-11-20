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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleSer;
        public RoleController(IRoleService roleSer) => _roleSer = roleSer;

        [HttpGet]
        [Route("portal/roles"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var roles = await _roleSer.GetRolePermissionServiceAsync(paging);
            if (roles.Data is null)
            {
                return NotFound(roles);
            }
            var metadata = new
            {
                roles.Data.TotalCount,
                roles.Data.PageSize,
                roles.Data.CurrentPage,
                roles.Data.TotalPages,
                roles.Data.HasNext,
                roles.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(roles);
        }
        [HttpGet]
        [Route("portal/permissions"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _roleSer.GetAllPermissionsServiceAsync();
            if (permissions.Data is null)
            {
                return NotFound();
            }
            return Ok(permissions);
        }

        [HttpGet]
        [Route("portal/roles/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var role = await _roleSer.GetByIdServiceAsync(id);
            if (role.Data is null)
            {
                return NotFound(role);
            }
            return Ok(role);
        }

        [HttpPost]
        [Route("portal/roles"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RoleDTO rolDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _roleSer.CreateServiceAsync(rolDto);
            if (role.Data is null)
            {
                return BadRequest(role);
            }
            return CreatedAtAction(nameof(Get), role);
        }

        [HttpPut]
        [Route("portal/roles/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] RoleDTO RoleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _roleSer.UpdateServiceAsync(id, RoleDTO);
            if (role.Data is null)
            {
                return NotFound(role);
            }
            return Ok(role);
        }

        [HttpDelete]
        [Route("portal/roles/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var role = await _roleSer.DeleteServiceAsync(id);
            if (role.Data is false)
            {
                return NotFound(role);
            }
            return NoContent();
        }
    }
}
