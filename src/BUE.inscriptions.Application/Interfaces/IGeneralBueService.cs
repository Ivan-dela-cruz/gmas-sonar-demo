using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IGeneralBueService
    {
        Task<IBaseResponse<PagedList<LevelDTO>>> getLevelsAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<CourseGradeDTO>>> getCourseGradeAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<SpecialtyDTO>>> getSpecialtiesAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<ParallelClassDTO>>> getParrallelsAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<NationalityDTO>>> getNationalitiesAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<CountryDTO>>> getCountriesAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<ProvinceDTO>>> getProvincesAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<CantonDTO>>> getCantonsAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<ParishDTO>>> getParishAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<RelationShipDTO>>> getRelationShipsAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<ProfessionDTO>>> getProfessionsAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<SchoolYearDTO>>> getSchoolYearsAsync(PagingQueryParameters paging);
    }
}
