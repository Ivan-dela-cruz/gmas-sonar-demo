using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IVoteRepository : IBaseRepository<VoteDTO>
    {
        Task<VoteDTO> GetByIdAsync(int id);
        Task<VoteDTO> GetByElectionAndUserIdAsync(int CandidateElectionId, int UserId);
        Task<IEnumerable<VoteDTO>> GetByUserIdAsync(int id);
        Task<IEnumerable<VoteCountDTO>> GetVoteCountsByCandidateElectionAsync(int ElectionId);
    }
}
