using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class StudentBUEService : IStudentBUEService
    {
        private IStudentBUERepository _studentRep;
        public StudentBUEService(IStudentBUERepository studentRep) => _studentRep = studentRep;
        public async Task<IBaseResponse<StudentBUEDTO>> CreateServiceAsync(StudentBUEDTO model)
        {
            var baseResponse = new BaseResponse<StudentBUEDTO>();
            var student = await _studentRep.CreateAsync(model);
            if (student is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = student;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(string id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _studentRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<StudentBUEDTO>> GetByIdServiceAsync(int code, int currentSchoolYear)
        {
            var baseResponse = new BaseResponse<StudentBUEDTO>();
            StudentBUEDTO student = await _studentRep.GetByIdAsync(code, currentSchoolYear);
            if (student is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = student;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<StudentBUEDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<StudentBUEDTO>>();
            var students = await _studentRep.GetAsync();
            if (students is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StudentBUEDTO>.ToPagedList(students, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<StudentBUEDTO>> UpdateServiceAsync(string id, StudentBUEDTO model)
        {

            var baseResponse = new BaseResponse<StudentBUEDTO>();
            StudentBUEDTO student = await _studentRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = student;
            return baseResponse;
        }
    }
}
