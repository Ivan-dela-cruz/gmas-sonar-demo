using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BUE.Inscriptions.Domain.Entity;

namespace BUE.Inscriptions.Domain.Elecctions.Entities
{
    [Table("OrganizationVotes")]
    public class OrganizationVote : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey("OrganizationElection")]
        [Column("organization_election_id")]
        public int? OrganizationElectionId { get; set; }
        [ForeignKey("Election")]
        [Column("election_id")]
        public int ElectionId { get; set; }
        [Column("vote_type")]
        public string VoteType { get; set; }
        [Column("vote_date")]
        public DateTime VoteDate { get; set; } 
        [Column("user_id")]
        public int UserId { get; set; }
        [ForeignKey("OrganizationElectionId")]
        public virtual OrganizationElection? OrganizationElection { get; set; }
        [ForeignKey("ElectionId")]
        public virtual Election Election { get; set; }
    }
}
