using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Permisos")]
    public class Permission : BaseEntity
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("Nombre")]
        public string name { get; set; }
        [Column("Idioma")]
        public string module { get; set; }
        [Column("Estado")]
        public bool? status { get; set; }
        [Column("Filtro")]
        public string? filter { get; set; }
        [Column("TipoObjeto")]
        public string? ObjectType { get; set; }
        [Column("Objeto")]
        public string? Object { get; set; }
        [Column("Configuracion")]
        public string? Config { get; set; }
    }
}
