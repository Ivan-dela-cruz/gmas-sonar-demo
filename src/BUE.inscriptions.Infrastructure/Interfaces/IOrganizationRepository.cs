using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IOrganizationRepository : IBaseRepository<OrganizationDTO>
    {
        Task<OrganizationDTO> GetByIdAsync(int id);
        Task<IEnumerable<OrganizationDTO>> GetByElectionIdAsync(int id);
    }
}
