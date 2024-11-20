using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Nivel")]
    public class LevelPortal
    {
        [Key]
        [Column("Codigo")]
        public int Code { get; set; }
        [Column("Nombre")]
        public string Name { get; set; }
        [Column("NombreIdiomaAlterno")]
        public string NameAlternative { get; set; }
        [Column("Estado")]
        public bool Status { get; set; }
        //public virtual ICollection<StudentPortal>? students { get; set; }
        //public virtual CourseGradePortal? courseGrade { get; set; }
    }
}
