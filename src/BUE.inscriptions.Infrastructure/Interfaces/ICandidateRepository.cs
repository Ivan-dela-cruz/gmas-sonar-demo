using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface ICandidateRepository: IBaseRepository<CandidateDTO>
    {
        Task<CandidateDTO> GetByIdAsync(int id);
        Task<IEnumerable<CandidateDTO>> GetByElectionIdAsync(int id);
    }
}
