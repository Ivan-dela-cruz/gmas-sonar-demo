using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IStudentRepresentativeRepository
    {
        Task<IEnumerable<StudentBUEDTO>> getContactStudentsAsyncBUE(int contactCode, int currentSchoolYear);
        Task<IEnumerable<ContactDTO>> GetAuthPeopleByStudentAsync(int studentCode, int currentSchoolYear);
        Task<ContactDTO> GetSecondContactByStudentAsync(int studentCode, int currentSchoolYear, int currentFirstRepresentative);
    }
}
