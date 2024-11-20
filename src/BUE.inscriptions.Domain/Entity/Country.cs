using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Pais")]
    public class Country
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("Nombre")]
        public string name { get; set; }
        [Column("Abreviatura")]
        public string abbreviation { get; set; }
        [Column("Estado")]
        public bool status { get; set; }
    }
}
