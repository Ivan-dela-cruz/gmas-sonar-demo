using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class CandidateElectionDTO
    {
        public int Id { get; set; }
        public int AcademicYearId { get; set; }
        public int ElectionId { get; set; }
        public int? OrganizationId { get; set; }
        public int PositionId { get; set; }
        public int CandidateId { get; set; }
        public bool Status { get; set; }
        public virtual ElectionDTO? Election { get; set; }
        public virtual OrganizationDTO? Organization { get; set; }
        public virtual PositionDTO? Position { get; set; }
        public virtual AcademicYearDTO? AcademicYear { get; set; }
        public virtual CandidateDTO? Candidate { get; set; }
    }
}
