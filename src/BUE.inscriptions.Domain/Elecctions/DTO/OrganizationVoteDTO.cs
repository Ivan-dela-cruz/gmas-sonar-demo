using BUE.Inscriptions.Domain.Elecctions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class OrganizationVoteDTO
    {
        public int Id { get; set; }
        public int? OrganizationElectionId { get; set; }
        public int ElectionId { get; set; }
        public string? VoteType { get; set; }
        public DateTime? VoteDate { get; set; }
        public int UserId { get; set; }
        public virtual OrganizationElection? OrganizationElection { get; set; }
        public virtual Election? Election { get; set; }
    }
}
