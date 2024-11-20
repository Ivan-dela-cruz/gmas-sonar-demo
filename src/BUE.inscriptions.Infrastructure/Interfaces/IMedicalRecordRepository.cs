using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IMedicalRecordRepository : IBaseRepository<MedicalRecordDTO>
    {
        Task<MedicalRecordDTO> GetByIdAsync(int id);
    }
}
