using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Inscriptions.Entities
{
    [Table("StudentRelatives")]
    public class StudentFamilies
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [ForeignKey("Student")]
        [Column("StudentId")]
        public int StudentId { get; set; }
        [ForeignKey("Person")]
        [Column("PersonId")]
        public int PersonId { get; set; }
        [Column("RelationTypeId")]
        public int RelationTypeId { get; set; }
        [Column("IsPrincipalRepresentative")]
        public bool IsPrincipalRepresentative { get; set; }
        [Column("IsEconomicRepresentative")]
        public bool IsEconomicRepresentative { get; set; }
        [Column("IsLegalRepresentative")]
        public bool IsLegalRepresentative { get; set; }
        [Column("IsResponsibleRepresentative")]
        public bool IsResponsibleRepresentative { get; set; }
        [Column("LivesWithStudent")]
        public bool LivesWithStudent { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person? Person { get; set; }
    }
}
