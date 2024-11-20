using BUE.Inscriptions.Domain.Elecctions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class OrganizationElectionDTO
    {
        public int Id { get; set; }
        public int ElectionId { get; set; }
        public int OrganizationId { get; set; }
        public bool Status { get; set; }
        public virtual ElectionDTO? Election { get; set; }
        public virtual OrganizationDTO? Organization { get; set; }
    }
}
