using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BUE.Inscriptions.Domain.Elecctions.DTO;

namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
    public class AcademicYearDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? FolioNumber { get; set; }
        public bool Status { get; set; }
        public bool? IsCurrent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BueId { get; set; }
        public string? IdExternal { get; set; }

        // Related Model DTOs
        public List<ElectionDTO>? Elections { get; set; }
    }
}
