using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        [Column("Codigo")]
        [Key]
        public int code { get; set; }
        [Column("Nombre")]
        public string name { get; set; }
        [Column("Descripcion")]
        public string? description { get; set; }
        [Column("Estado")]
        public bool? status { get; set; }
        public virtual ICollection<UserRole> userRoles { get; set; }
        public virtual IEnumerable<Permission>? permissions { get; set; }
    }
}
