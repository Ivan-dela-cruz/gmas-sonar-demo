using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IMedicalRecordService :IBaseService<MedicalRecordDTO>
    {
        Task<IBaseResponse<MedicalRecordDTO>> GetByIdServiceAsync(int id);
    }
  
}
