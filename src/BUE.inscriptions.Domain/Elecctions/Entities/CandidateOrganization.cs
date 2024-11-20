using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BUE.Inscriptions.Domain.Entity;

namespace BUE.Inscriptions.Domain.Elecctions.Entities
{
    [Table("CandidateOrganization")]
    public class CandidateOrganization : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey("Organization")]
        [Column("organization_id")]
        public int OrganizationId { get; set; }
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("status")]
        public bool Status { get; set; }

        [Column("is_representative")]
        public bool IsRepresentative { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization? Organization { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        
    }
}
