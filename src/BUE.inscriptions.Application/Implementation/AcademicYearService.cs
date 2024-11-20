using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public AcademicYearService(IAcademicYearRepository academicYearRepository)
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<IBaseResponse<AcademicYearDTO>> CreateServiceAsync(AcademicYearDTO model)
        {
            var baseResponse = new BaseResponse<AcademicYearDTO>();
            var academicYear = await _academicYearRepository.CreateAsync(model);
            baseResponse.Data = academicYear;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _academicYearRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<AcademicYearDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<AcademicYearDTO>();
            AcademicYearDTO academicYear = await _academicYearRepository.GetByIdAsync(id);

            if (academicYear == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = academicYear;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<AcademicYearDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<AcademicYearDTO>>();
            var academicYears = await _academicYearRepository.GetAsync();

            if (academicYears == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<AcademicYearDTO>.ToPagedList(academicYears, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<AcademicYearDTO>> UpdateServiceAsync(int id, AcademicYearDTO model)
        {
            var baseResponse = new BaseResponse<AcademicYearDTO>();
            AcademicYearDTO academicYear = await _academicYearRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = academicYear;
            return baseResponse;
        }
    }
}
