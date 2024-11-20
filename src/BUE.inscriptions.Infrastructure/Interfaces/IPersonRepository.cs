using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IPersonRepository : IBaseRepository<PersonDTO>
    {
        public string Message { get; set; }
        public string StatusCode { get; set; }
        Task<PersonDTO> GetByIdAsync(int id);
        Task<PersonDTO> GetByIdentificationAsync(string identification);
    }
}
