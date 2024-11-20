using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class VoteDistributionDTO<T>
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int Votes { get; set; }
        public int SeatsWon { get; set; }
        public T? Reference { get; set; }
        public List<CandidateMembersDTO>? Members { get; set; }
        public List<CandidateMembersDTO>? Distribution { get; set; }
        public VoteDistributionDTO(string listName, int votes)
        {
            OrganizationName = listName;
            Votes = votes;
            SeatsWon = 0;
        }
    }
    public class CandidateMembersDTO
    {
        public string? Name { get; set; }
        public string? Identification { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
       
    }
}
