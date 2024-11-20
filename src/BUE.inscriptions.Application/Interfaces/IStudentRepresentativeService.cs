using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IStudentRepresentativeService
    {
        Task<IBaseResponse<PagedList<StudentBUEDTO>>> getContactStudentsBUEService(PagingQueryParameters paging, int code, int currentSchoolYear);
        Task<IBaseResponse<PagedList<ContactDTO>>> GetAuthPeopleByStudentAsync(PagingQueryParameters paging, int studentCode, int currentSchoolYear);
        Task<IBaseResponse<ContactDTO>> GetSecondContactByStudentAsync(int studentCode, int currentSchoolYear, int currentFirstRepresentative);
    }
}
