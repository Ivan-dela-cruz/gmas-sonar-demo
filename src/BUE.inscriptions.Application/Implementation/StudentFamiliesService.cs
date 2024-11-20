using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class StudentFamiliesService : IStudentFamiliesService
    {
        private readonly IStudentFamiliesRepository _studentRelativesRepository;

        public StudentFamiliesService(IStudentFamiliesRepository studentRelativesRepository)
        {
            _studentRelativesRepository = studentRelativesRepository;
        }

        public async Task<IBaseResponse<StudentFamiliesDTO>> CreateServiceAsync(StudentFamiliesDTO model)
        {
            var baseResponse = new BaseResponse<StudentFamiliesDTO>();
            var studentRelatives = await _studentRelativesRepository.CreateAsync(model);
            if (studentRelatives == null)
            {
                baseResponse.Message = _studentRelativesRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _studentRelativesRepository.StatusCode;
            }
            baseResponse.Data = studentRelatives;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _studentRelativesRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<StudentFamiliesDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<StudentFamiliesDTO>();
            StudentFamiliesDTO studentRelatives = await _studentRelativesRepository.GetByIdAsync(id);

            if (studentRelatives == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = studentRelatives;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<StudentFamiliesDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<StudentFamiliesDTO>>();
            var studentRelativess = await _studentRelativesRepository.GetAsync();

            if (studentRelativess == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StudentFamiliesDTO>.ToPagedList(studentRelativess, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<StudentFamiliesDTO>> UpdateServiceAsync(int id, StudentFamiliesDTO model)
        {
            var baseResponse = new BaseResponse<StudentFamiliesDTO>() { Message = MessageUtil.Instance.Updated };
            StudentFamiliesDTO studentRelatives = await _studentRelativesRepository.UpdateAsync(id, model);
            if (studentRelatives == null)
            {
                baseResponse.Message = _studentRelativesRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _studentRelativesRepository.StatusCode;
            }
            baseResponse.Data = studentRelatives;
            return baseResponse;
        }
    }
}
