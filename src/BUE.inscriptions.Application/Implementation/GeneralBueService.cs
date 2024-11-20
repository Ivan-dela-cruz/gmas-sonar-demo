using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class GeneralBueService : IGeneralBueService
    {
        private readonly IGenaralBueRepository _generalBueRep;
        public GeneralBueService(IGenaralBueRepository generalBueRep) => _generalBueRep = generalBueRep;

        public async Task<IBaseResponse<PagedList<CourseGradeDTO>>> getCourseGradeAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CourseGradeDTO>>();
            var courses = await _generalBueRep.getCoursesAsync();
            if (courses is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<CourseGradeDTO>.ToPagedList(courses, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<LevelDTO>>> getLevelsAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<LevelDTO>>();
            var levels = await _generalBueRep.getLevelAsync();
            if (levels is null)
            {
                baseResponse.status = false;
                baseResponse.Message = MessageUtil.Instance.Empty;
            }
            baseResponse.Data = PagedList<LevelDTO>.ToPagedList(levels, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<ParallelClassDTO>>> getParrallelsAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<ParallelClassDTO>>();
            var parallels = await _generalBueRep.getParallesAsync();
            if (parallels is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<ParallelClassDTO>.ToPagedList(parallels, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<SpecialtyDTO>>> getSpecialtiesAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<SpecialtyDTO>>();
            var specialties = await _generalBueRep.getSpecialtiesAsync();
            if (specialties is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<SpecialtyDTO>.ToPagedList(specialties, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<NationalityDTO>>> getNationalitiesAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<NationalityDTO>>();
            var nationalities = await _generalBueRep.getNationalitiesAsync();
            if (nationalities is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<NationalityDTO>.ToPagedList(nationalities, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<CountryDTO>>> getCountriesAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CountryDTO>>();
            var countries = await _generalBueRep.getCountriesAsync();
            if (countries is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<CountryDTO>.ToPagedList(countries, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<ProvinceDTO>>> getProvincesAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<ProvinceDTO>>();
            var provinces = await _generalBueRep.getProvincesAsync();
            if (provinces is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<ProvinceDTO>.ToPagedList(provinces, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<CantonDTO>>> getCantonsAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CantonDTO>>();
            var cantons = await _generalBueRep.getCantonsAsync();
            if (cantons is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<CantonDTO>.ToPagedList(cantons, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<ParishDTO>>> getParishAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<ParishDTO>>();
            var parishes = await _generalBueRep.getParishAsync();
            if (parishes is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<ParishDTO>.ToPagedList(parishes, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<RelationShipDTO>>> getRelationShipsAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<RelationShipDTO>>();
            var relations = await _generalBueRep.getRelationShipsAsync();
            if (relations is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<RelationShipDTO>.ToPagedList(relations, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<ProfessionDTO>>> getProfessionsAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<ProfessionDTO>>();
            var professions = await _generalBueRep.getProfessionsAsync();
            if (professions is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<ProfessionDTO>.ToPagedList(professions, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<SchoolYearDTO>>> getSchoolYearsAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<SchoolYearDTO>>();
            var schoolYears = await _generalBueRep.getSchoolYearsAsync();
            if (schoolYears is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<SchoolYearDTO>.ToPagedList(schoolYears, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
    }
}
