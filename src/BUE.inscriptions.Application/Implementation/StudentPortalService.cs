using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using BUE.Inscriptions.Domain.Request;

namespace BUE.Inscriptions.Application.Implementation
{
    public class StudentPortalService : IStudentPortalService
    {
        private IStudentPortalRepository _studentRep;
        private IStudentBUERepository _studentBue;
        private IMapper _mapper;
        private IPortalRequestRepository _requestRep;
        public StudentPortalService(IStudentPortalRepository studentRep, IStudentBUERepository studentBue, IMapper mapper, IPortalRequestRepository requestRep)
        {
            _studentRep = studentRep;
            _studentBue = studentBue;
            _mapper = mapper;
            _requestRep = requestRep;
        }
        public async Task<IBaseResponse<StudentPortalDTO>> CreateServiceAsync(StudentPortalDTO model)
        {
            var baseResponse = new BaseResponse<StudentPortalDTO>();
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
        public async Task<IBaseResponse<StudentPortalDTO>> GetByIdServiceAsync(int code, int currentSchoolYear)
        {
            var baseResponse = new BaseResponse<StudentPortalDTO>();
            StudentPortalDTO student = await _studentRep.GetByIdAsync(code, currentSchoolYear);
            if (student is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = student;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<StudentPortalDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<StudentPortalDTO>>();
            var students = await _studentRep.GetAsync();
            if (students is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StudentPortalDTO>.ToPagedList(students, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<StudentPortalDTO>>> GetListBasicServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<StudentPortalDTO>>();
            var students = await _studentRep.GetListBasicAsync();
            if (students is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StudentPortalDTO>.ToPagedList(students, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<StudentPortalDTO>> UpdateServiceAsync(int id, StudentPortalDTO model)
        {

            var baseResponse = new BaseResponse<StudentPortalDTO>();
            StudentPortalDTO student = await _studentRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = student;
            return baseResponse;
        }

        public async Task<IBaseResponse<StudentBUEDTO>> CrearteToBueServiceAsync(PortalRequestDTO model)
        {
            var baseResponse = new BaseResponse<StudentBUEDTO>();
            var entity = await _studentRep.GetByPrimaryKeyAsync((int)model.studentCodeSchoolYear);
            if (entity is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            var studentDto = _mapper.Map<StudentPortalDTO, StudentBUEDTO>(entity);
            studentDto.enrollmentDate = DateTime.Now;
            var student = await _studentBue.CreateAsync(studentDto);
            if (student is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.Data = student;
            return baseResponse;
        }

        public async Task<IBaseResponse<StudentPortalDTO>> UpdateEnrollmentAppServiceAsync(int id, StudentPortalDTO model)
        {

            var baseResponse = new BaseResponse<StudentPortalDTO>();
            StudentPortalDTO student = await _studentRep.UpdateDataEnrollmentAppAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = student;
            return baseResponse;
        }
                
        public async Task<IBaseResponse<IEnumerable<StudentPortalDTO>>> GetStudentsByCodeAsync(IEnumerable<HelperFieldValidate> helperFields)
        {
            var baseResponse = new BaseResponse<IEnumerable<StudentPortalDTO>>();
            var students = await _studentRep.GetStudentsByCodeAsync(helperFields);
            if (students is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = students;
            return baseResponse;
        }
    }
}
