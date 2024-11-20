using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BUE.Inscriptions.Domain.Entity;

namespace BUE.Inscriptions.Domain.Elecctions.Entities
{
    [Table("OrganizationElection")]
    public class OrganizationElection : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Election")]
        [Column("election_id")]
        public int ElectionId { get; set; }

        [ForeignKey("Organization")]
        [Column("organization_id")]
        public int? OrganizationId { get; set; }

        
        [Column("status")]
        public bool Status { get; set; }

        [ForeignKey("ElectionId")]
        public virtual Election? Election { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization? Organization { get; set; }
    }
}
