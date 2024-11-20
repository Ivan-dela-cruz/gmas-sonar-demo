using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IInscriptionRepository : IBaseRepository<InscriptionDTO>
    {
        public string Message {  get; set; }
        public string StatusCode {  get; set; }
        Task<InscriptionDTO> GetByIdAsync(int id);
        Task<InscriptionDTO> UpdateProcessAsync(int id, InscriptionDTO entity);
        Task<InscriptionDTO> UpdateSingContractAsync(SingFileDTO modelDto, byte[] fileBytes, string IdEvicertia);
        Task<IEnumerable<StudentDTO>> GetByAcademicYearAsync(PagingQueryParameters paging);
        Task<bool> UpdateStatusAsync(RequestStatusChange requestStatus);
        Task<InscriptionDTO> UpdateSingsAsync(int id, InscriptionDTO entity, string type = "");
    }
}
