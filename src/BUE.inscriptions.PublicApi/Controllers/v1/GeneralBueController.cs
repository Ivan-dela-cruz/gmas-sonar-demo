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
    public class GeneralBueController : ControllerBase
    {
        private readonly IGeneralBueService _generalSer;
        public GeneralBueController(IGeneralBueService generalSer) => _generalSer = generalSer;

        [HttpGet("bue/levels"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLevels([FromQuery] PagingQueryParameters paging)
        {
            var levels = await _generalSer.getLevelsAsync(paging);
            if (levels.Data is null)
            {
                return NotFound(levels);
            }
            var metadata = new
            {
                levels.Data.TotalCount,
                levels.Data.PageSize,
                levels.Data.CurrentPage,
                levels.Data.TotalPages,
                levels.Data.HasNext,
                levels.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(levels);
        }
        [HttpGet("bue/courses"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCourses([FromQuery] PagingQueryParameters paging)
        {
            var courses = await _generalSer.getCourseGradeAsync(paging);
            if (courses.Data is null)
            {
                return NotFound(courses);
            }
            var metadata = new
            {
                courses.Data.TotalCount,
                courses.Data.PageSize,
                courses.Data.CurrentPage,
                courses.Data.TotalPages,
                courses.Data.HasNext,
                courses.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(courses);
        }
        [HttpGet("bue/parallels"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetParallels([FromQuery] PagingQueryParameters paging)
        {
            var parallels = await _generalSer.getParrallelsAsync(paging);
            if (parallels.Data is null)
            {
                return NotFound(parallels);
            }
            var metadata = new
            {
                parallels.Data.TotalCount,
                parallels.Data.PageSize,
                parallels.Data.CurrentPage,
                parallels.Data.TotalPages,
                parallels.Data.HasNext,
                parallels.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(parallels);
        }
        [HttpGet("bue/specialties"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSepecialties([FromQuery] PagingQueryParameters paging)
        {
            var specialties = await _generalSer.getSpecialtiesAsync(paging);
            if (specialties.Data is null)
            {
                return NotFound(specialties);
            }
            var metadata = new
            {
                specialties.Data.TotalCount,
                specialties.Data.PageSize,
                specialties.Data.CurrentPage,
                specialties.Data.TotalPages,
                specialties.Data.HasNext,
                specialties.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(specialties);
        }
        [HttpGet("bue/nationalities"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNationalities([FromQuery] PagingQueryParameters paging)
        {
            var nationalities = await _generalSer.getNationalitiesAsync(paging);
            if (nationalities.Data is null)
            {
                return NotFound(nationalities);
            }
            var metadata = new
            {
                nationalities.Data.TotalCount,
                nationalities.Data.PageSize,
                nationalities.Data.CurrentPage,
                nationalities.Data.TotalPages,
                nationalities.Data.HasNext,
                nationalities.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(nationalities);
        }
        [HttpGet("bue/countries"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountries([FromQuery] PagingQueryParameters paging)
        {
            var countries = await _generalSer.getCountriesAsync(paging);
            if (countries.Data is null)
            {
                return NotFound(countries);
            }
            var metadata = new
            {
                countries.Data.TotalCount,
                countries.Data.PageSize,
                countries.Data.CurrentPage,
                countries.Data.TotalPages,
                countries.Data.HasNext,
                countries.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(countries);
        }
        [HttpGet("bue/provinces"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPronvinces([FromQuery] PagingQueryParameters paging)
        {
            var provinces = await _generalSer.getProvincesAsync(paging);
            if (provinces.Data is null)
            {
                return NotFound(provinces);
            }
            var metadata = new
            {
                provinces.Data.TotalCount,
                provinces.Data.PageSize,
                provinces.Data.CurrentPage,
                provinces.Data.TotalPages,
                provinces.Data.HasNext,
                provinces.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(provinces);
        }
        [HttpGet("bue/cantons"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCantons([FromQuery] PagingQueryParameters paging)
        {
            var cantons = await _generalSer.getCantonsAsync(paging);
            if (cantons.Data is null)
            {
                return NotFound(cantons);
            }
            var metadata = new
            {
                cantons.Data.TotalCount,
                cantons.Data.PageSize,
                cantons.Data.CurrentPage,
                cantons.Data.TotalPages,
                cantons.Data.HasNext,
                cantons.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(cantons);
        }
        [HttpGet("bue/parishes"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetParishes([FromQuery] PagingQueryParameters paging)
        {
            var parishes = await _generalSer.getParishAsync(paging);
            if (parishes.Data is null)
            {
                return NotFound(parishes);
            }
            var metadata = new
            {
                parishes.Data.TotalCount,
                parishes.Data.PageSize,
                parishes.Data.CurrentPage,
                parishes.Data.TotalPages,
                parishes.Data.HasNext,
                parishes.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(parishes);
        }
        [HttpGet("bue/relations-ship"), Authorize]
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
        [HttpGet("bue/professions"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProfessions([FromQuery] PagingQueryParameters paging)
        {
            var professions = await _generalSer.getProfessionsAsync(paging);
            if (professions.Data is null)
            {
                return NotFound(professions);
            }
            var metadata = new
            {
                professions.Data.TotalCount,
                professions.Data.PageSize,
                professions.Data.CurrentPage,
                professions.Data.TotalPages,
                professions.Data.HasNext,
                professions.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(professions);
        }
        [HttpGet("bue/school-years"), Authorize]
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
    }
}
