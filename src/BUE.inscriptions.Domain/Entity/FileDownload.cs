using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("ArchivoDescargas")]
    public class FileDownload : BaseEntity
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("Nombre")]
        public string name { get; set; }
        [Column("Descripcion")]
        public string? description { get; set; }
        [Column("Estado")]
        public bool status { get; set; }
        [Column("Identificador")]
        public string identification { get; set; }
        [Column("urlFile")]
        public string? urlFile { get; set; }
        [Column("CodigoCursoGrado")]
        public int? courseCode { get; set; }
        //[Column("Archivo")]
        //public byte[]? file { get; set; }
        [Column("NombreIdiomas")]
        public string? langNames { get; set; }
        [Column("Modulo")]
        public string? Module { get; set; }
        [Column("CodigoIdioma")]
        public string? LangCode { get; set; }
    }
}
