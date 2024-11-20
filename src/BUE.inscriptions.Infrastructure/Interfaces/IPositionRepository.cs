using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IPositionRepository : IBaseRepository<PositionDTO>
    {
        Task<PositionDTO> GetByIdAsync(int id);
    }
}
