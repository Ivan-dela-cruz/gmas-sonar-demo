using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IOrganizationElectionRepository : IBaseRepository<OrganizationElectionDTO>
    {
        Task<OrganizationElectionDTO> GetByIdAsync(int id);
        Task<IEnumerable<OrganizationElectionDTO>> GetByElectionIdAsync(int id);
    }
}
