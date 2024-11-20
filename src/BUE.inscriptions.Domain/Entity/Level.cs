using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Nivel")]
    public class Level
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
        public virtual ICollection<StudentBUE>? students { get; set; }
    }
}
