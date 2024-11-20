using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface ICandidateElectionRepository : IBaseRepository<CandidateElectionDTO>
    {
        Task<CandidateElectionDTO> GetByIdAsync(int id);
        Task<IEnumerable<CandidateElectionDTO>> GetByElectionIdAsync(int id);
    }
}
