using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IElectionRepository: IBaseRepository<ElectionDTO>
    {
        Task<ElectionDTO> GetByIdAsync(int id);
        Task<int> GetTotalUsersAsync(int electionId);
        Task<IEnumerable<int>> GetUserLevelElectionAsync(int userId);
    }
}
