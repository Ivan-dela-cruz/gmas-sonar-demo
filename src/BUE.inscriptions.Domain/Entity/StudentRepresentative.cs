using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("ContactoEstudianteRepresentante")]
    public class StudentRepresentative
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("CodigoEstudianteAnioLectivo")]
        public int studentCodeSchoolYear { get; set; }
        [Column("CodigoContacto")]
        public int contactCode { get; set; }
        [Column("EsRepresentanteLegal")]
        public bool isLegalRepresentative { get; set; }
        [Column("EsResponsable")]
        public bool isResponsible { get; set; }
        [Column("CodigoParentesco")]
        public int relationshipStudentCode { get; set; }
        [Column("CodigoEvento")]
        public int eventCode { get; set; }
        [Column("FechaActualizacion")]
        public DateTime? updatedAt { get; set; }
        [Column("UsuarioActualizacion")]
        public string? userUpdate { get; set; }
        public virtual Contact contact { get; set; }
        public virtual StudentBUE student { get; set; }
    }
}
