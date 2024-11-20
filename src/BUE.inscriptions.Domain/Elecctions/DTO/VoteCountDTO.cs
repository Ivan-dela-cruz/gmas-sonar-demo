using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class VoteCountDTO
    {
        public int CandidateElectionId { get; set; }
        public int? OrganizationElectionId { get; set; }
        public int ElectionId { get; set; }
        public CandidateDTO? Candidate { get; set; }
        public OrganizationDTO? Organization { get; set; }
        public ElectionDTO? Election { get; set; }
        public PositionDTO? Position { get; set; }
        public int VoteCount { get; set; }
    }
}
