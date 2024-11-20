using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Inscriptions.Entities;

namespace BUE.Inscriptions.Domain.Elecctions.Entities
{
    [Table("CandidateElection")]
    public class CandidateElection : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("AcademicYear")]
        [Column("academic_years_id")]
        public int AcademicYearId { get; set; }

        [ForeignKey("Election")]
        [Column("election_id")]
        public int ElectionId { get; set; }

        ////[ForeignKey("Organization")]
        [Column("organization_id")]
        public int? OrganizationId { get; set; }

        [ForeignKey("Position")]
        [Column("position_id")]
        public int PositionId { get; set; }

        [ForeignKey("Candidate")]
        [Column("candidate_id")]
        public int CandidateId { get; set; }
        
        [Column("status")]
        public bool Status { get; set; }

        [ForeignKey("ElectionId")]
        public virtual Election? Election { get; set; }

        //[ForeignKey("OrganizationId")]
        public virtual Organization? Organization { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position? Position { get; set; }

        [ForeignKey("AcademicYearId")]
        public virtual AcademicYear? AcademicYear { get; set; }  
        [ForeignKey("CandidateId")]
        public virtual Candidate? Candidate { get; set; }
    }
}
