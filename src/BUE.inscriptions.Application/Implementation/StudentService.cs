using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using System.Drawing.Printing;


namespace BUE.Inscriptions.Application.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IInscriptionRepository _inscriptionRepository;

        public StudentService(IStudentRepository studentRepository, IInscriptionRepository inscriptionRepository)
        {
            _studentRepository = studentRepository;
            _inscriptionRepository = inscriptionRepository;
        }

        public async Task<IBaseResponse<StudentDTO>> CreateServiceAsync(StudentDTO model)
        {
            var baseResponse = new BaseResponse<StudentDTO>();

            var student = await _studentRepository.CreateAsync(model);
            if (student == null)
            {
                baseResponse.Message = _studentRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _studentRepository.StatusCode;
                return baseResponse;
            }
            baseResponse.Data = student;
            if (model.Inscriptions != null && model.Inscriptions.Count() == 1)
            {
                var inscription = model.Inscriptions.First();
                inscription.StudentId = student.Id;
                var resultInscription = await _inscriptionRepository.CreateAsync(inscription);
                if (resultInscription != null)
                {
                    var studentData = await _studentRepository.GetByIdAsync(student.Id);
                    baseResponse.Data = studentData;
                }
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _studentRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<StudentDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<StudentDTO>();
            StudentDTO student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = student;
            return baseResponse;
        }
        public async Task<IBaseResponse<StudentDetails>> GetStudentDetailsServiceAsync(int studentId, int academicYearId)
        {
            var baseResponse = new BaseResponse<StudentDetails>();
            var details = await _studentRepository.GetStudentDetailsAsync(studentId, academicYearId);

            if (details == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = details;
            return baseResponse;
        }
       

        public async Task<IBaseResponse<PagedList<StudentDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<StudentDTO>>();
            var students = await _studentRepository.GetAsync();

            if (students == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StudentDTO>.ToPagedList(students, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<StudentDTO>>> GetByAcademicYearAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<StudentDTO>>();
            var students = await _studentRepository.GetByAcademicYearAsync((int)paging.academicYear, 1);

            if (students == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StudentDTO>.ToPagedList(students, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<StudentDTO>>> GetByUserIdAsync(PagingQueryParameters paging, int userId)
        {
            var baseResponse = new BaseResponse<PagedList<StudentDTO>>();
            var students = await _studentRepository.GetByUserIdAsync(userId, 1);

            if (students == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StudentDTO>.ToPagedList(students, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<StudentDTO>> UpdateServiceAsync(int id, StudentDTO model)
        {
            var baseResponse = new BaseResponse<StudentDTO>() { Message = MessageUtil.Instance.Updated };
            StudentDTO student = await _studentRepository.UpdateAsync(id, model);
            if (student == null)
            {
                baseResponse.Message = _studentRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _studentRepository.StatusCode;
            }
            baseResponse.Data = student;
            return baseResponse;
        }
        public async Task<IBaseResponse<StudentDTO>> UpdateImageServiceAsync(int externalId, FileStorageDTO filesStorage)
        {
            var baseResponse = new BaseResponse<StudentDTO>() { Message = MessageUtil.Instance.Updated };
            StudentDTO student = await _studentRepository.UpdateImageBueAsync(externalId, filesStorage);
            if (student == null)
            {
                baseResponse.Message = _studentRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _studentRepository.StatusCode;
            }
            baseResponse.Data = student;
            return baseResponse;
        }
    }
}
