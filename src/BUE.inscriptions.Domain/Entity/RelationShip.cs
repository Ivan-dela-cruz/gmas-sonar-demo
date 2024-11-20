using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Parentesco")]
    public class RelationShip
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("Nombre")]
        public string name { get; set; }
        [Column("Estado")]
        public bool status { get; set; }
    }
}
