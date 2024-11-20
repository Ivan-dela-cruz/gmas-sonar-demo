using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Estados")]
    public class Status : BaseEntity
    {
        [Column("Codigo")]
        [Key]
        public int code { get; set; }
        [Column("Nombre")]
        public string name { get; set; }
        [Column("NombreIdiomas")]
        public string lang { get; set; }
        [Column("UrlPagina")]
        public string? UrlPage { get; set; }
        [Column("Valor")]
        public int value { get; set; }
        [Column("Proceso")]
        public string? process { get; set; }
        [Column("Estado")]
        public bool status { get; set; }
        [Column("Orden")]
        public int? Order { get; set; }
    }
}
