using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IOrganizationVoteRepository : IBaseRepository<OrganizationVoteDTO>
    {
        Task<OrganizationVoteDTO> GetByIdAsync(int id);
        Task<OrganizationVoteDTO> GetByElectionAndUserIdAsync(int CandidateElectionId, int UserId);
        Task<IEnumerable<OrganizationVoteDTO>> GetByUserIdAsync(int id);
        Task<IEnumerable<VoteCountDTO>> GetCountsOrganizationVoteByElectionAsync(int ElectionId);
        Task<int> GeOrganizationBlankVoteByElectionAsync(int ElectionId);
    }
}
