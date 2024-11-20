using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Notificacion")]
    public class Notification : BaseEntity
    {
        [Column("Codigo")]
        [Key]
        public int Code { get; set; }
        [Column("Titulo")]
        public string Title { get; set; }
        [Column("NombreTemplate")]
        public string TemplateName { get; set; }
        [Column("Asunto")]
        public string Subject { get; set; }
        [Column("Tipo")]
        public string Type { get; set; }
        [Column("Descripcion")]
        public string Description { get; set; }
        [Column("Template")]
        public string Template { get; set; }
        [Column("Estado")]
        public bool status { get; set; }
        [Column("Idioma")]
        public string Language { get; set; }
    }
}
