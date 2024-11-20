using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("EstadoCivil")]
    public class CivilStatus
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }

        [Column("Nombre")]
        public string name { get; set; }
    }
}
