using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IStudentFamiliesService : IBaseService<StudentFamiliesDTO>
    {
        Task<IBaseResponse<StudentFamiliesDTO>> GetByIdServiceAsync(int id);
    }
}
