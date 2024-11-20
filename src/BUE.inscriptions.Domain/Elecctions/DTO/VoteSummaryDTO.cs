using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class VoteSummaryDTO<T>
    {
        public int Inscriptions { get; set; }
        public int BlankVotes { get; set; }
        public int TotalVotes { get; set; }
        public int ValidVotes { get; set; }
        public decimal Quota { get; set; }
        public List<VoteDistributionDTO<T>>? VoteDistributions { get; set; } 

    }
}
