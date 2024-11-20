using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface ICandidateOrganizationRepository : IBaseRepository<CandidateOrganizationDTO>
    {
        Task<CandidateOrganizationDTO> GetByIdAsync(int id);
        Task<IEnumerable<CandidateOrganizationDTO>> GetByOrganizationIdAsync(int id);
        Task<bool> DeleteOrganizationCandidateAsync(int organizationId, int candidateId);
    }
}
