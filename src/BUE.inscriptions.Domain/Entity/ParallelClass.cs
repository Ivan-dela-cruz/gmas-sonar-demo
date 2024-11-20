using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Paralelo")]
    public class ParallelClass
    {
        [Key]
        [Column("Codigo")]
        public int Code { get; set; }
        [Column("Nombre")]
        public string Name { get; set; }
        [Column("Estado")]
        public bool Status { get; set; }
    }
}
