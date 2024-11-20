using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class StudentRepresentativeService : IStudentRepresentativeService
    {
        private IStudentRepresentativeRepository _stRRep;
        private IStudentBUERepository _studentRep;
        public StudentRepresentativeService(IStudentRepresentativeRepository stRRep, IStudentBUERepository studentRep)
        {
            _stRRep = stRRep;
            _studentRep = studentRep;
        }

        public async Task<IBaseResponse<PagedList<ContactDTO>>> GetAuthPeopleByStudentAsync(PagingQueryParameters paging, int studentCode, int currentSchoolYear)
        {
            var baseResponse = new BaseResponse<PagedList<ContactDTO>>();

            var contacts = await _stRRep.GetAuthPeopleByStudentAsync(studentCode, currentSchoolYear);
            if (contacts is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            else
                baseResponse.Data = PagedList<ContactDTO>.ToPagedList(contacts, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<StudentBUEDTO>>> getContactStudentsBUEService(PagingQueryParameters paging, int code, int currentSchoolYear)
        {
            var baseResponse = new BaseResponse<PagedList<StudentBUEDTO>>();

            var students = await _stRRep.getContactStudentsAsyncBUE(code, currentSchoolYear);
            if (students is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StudentBUEDTO>.ToPagedList(students, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<ContactDTO>> GetSecondContactByStudentAsync(int studentCode, int currentSchoolYear, int currentFirstRepresentative)
        {
            var baseResponse = new BaseResponse<ContactDTO>();

            var contact = await _stRRep.GetSecondContactByStudentAsync(studentCode, currentSchoolYear, currentFirstRepresentative);
            if (contact is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            else
                baseResponse.Data = contact;
            return baseResponse;
        }

    }
}
