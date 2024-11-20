using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Usuarios_Roles")]
    public class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Codigo")]
        public int? code { get; set; }
        [Column("codigoRol")]
        public int roleCode { get; set; }
        [Column("codigoUsuario")]
        public int userCode { get; set; }
        public virtual Role role { get; set; }
        public virtual User user { get; set; }
    }
}
