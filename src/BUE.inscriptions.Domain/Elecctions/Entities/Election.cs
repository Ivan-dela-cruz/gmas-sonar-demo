using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Inscriptions.Entities;

namespace BUE.Inscriptions.Domain.Elecctions.Entities
{
    [Table("Elections")]
    public class Election : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("AcademicYear")]
        [Column("academic_years_id")]
        public int AcademicYearId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        //[Column("date_election")]
        //public DateTime? DateElection { get; set; }

        [Column("start_datetime")]
        public DateTime? StartDateTime { get; set; }

        [Column("end_datetime")]
        public DateTime? EndDateTime { get; set; }

        [Column("cover_image")]
        public string CoverImage { get; set; }

        [Column("thumbnail_image")]
        public string ThumbnailImage { get; set; }

        [Column("election_type")]
        public string ElectionType { get; set; }

        [Column("academic_period")]
        public string AcademicPeriod { get; set; }

        [Column("year")]
        public int? Year { get; set; }

        [Column("results")]
        public string Results { get; set; }

        [Column("status")]
        public bool Status { get; set; }
        [Column("status_election")]
        public string StatusElection { get; set; } = "Abierto";
        [Column("seats")]
        public int Seats { get; set; } 
        [Column("group")]
        public int Group { get; set; }
        [ForeignKey("AcademicYearId")]
        public virtual AcademicYear AcademicYear { get; set; }
    }
}
