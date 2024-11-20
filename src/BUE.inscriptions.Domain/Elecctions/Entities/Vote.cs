using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BUE.Inscriptions.Domain.Entity;

namespace BUE.Inscriptions.Domain.Elecctions.Entities
{
    [Table("Votes")]
    public class Vote : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey("CandidateElection")]
        [Column("candidate_election_id")]
        public int CandidateElectionId { get; set; }
        [Column("vote_type")]
        public string VoteType { get; set; }
        [Column("vote_date")]
        public DateTime VoteDate { get; set; } 
        [Column("user_id")]
        public int UserId { get; set; }
        [ForeignKey("CandidateElectionId")]
        public virtual CandidateElection CandidateElection { get; set; }
    }
}
