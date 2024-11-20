using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("CursoGrado")]
    public class CourseGrade
    {
        [Key]
        [Column("Codigo")]
        public int Code { get; set; }
        [Column("CodigoNivel")]
        public int levelCode { get; set; }
        [Column("Nombre")]
        public string Name { get; set; }
        [Column("NombreIdiomaAlterno")]
        public string? NameAlternative { get; set; }
        [Column("Estado")]
        public bool Status { get; set; }
        [Column("CodigoCursoGradoSiguiente")]
        public int? NextCourse { get; set; }
        [Column("NombreSegundoIdioma")]
        public string? NameSecondLanguage { get; set; }
        [Column("NombreSegundoIdiomaAbreviado")]
        public string? NameSecondLanguageShort { get; set; }
        [Column("NivelSegundoIdioma")]
        public string? LevelSecondLanguage { get; set; }
        public virtual ICollection<StudentBUE>? students { get; set; }
    }
}
