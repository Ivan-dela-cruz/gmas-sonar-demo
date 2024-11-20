using BUE.Inscriptions.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Inscriptions.Entities
{
    [Table("AcademicYears")]
    public class AcademicYear : BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [Column("FolioNumber")]
        public string? FolioNumber { get; set; }

        [Column("Status")]
        public bool Status { get; set; }

        [Column("IsCurrent")]
        public bool? IsCurrent { get; set; }

        [Column("StartDate")]
        public DateTime? StartDate { get; set; }

        [Column("EndDate")]
        public DateTime? EndDate { get; set; }

        [Column("BueId")]
        public int? BueId { get; set; }

        [Column("IdExternal")]
        public string? IdExternal { get; set; }
    }
}
