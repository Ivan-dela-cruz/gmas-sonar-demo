using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IAcademicYearRepository: IBaseRepository<AcademicYearDTO>
    {
        Task<AcademicYearDTO> GetByIdAsync(int id);
    }
}
