using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Padron.Entities
{
    [Table("Padron")]
    public class Padron
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("UserId")]
        public int? UserId { get; set; }
        [Column("ParentId")]
        public int? ParentId { get; set; }
        [Column("StudentId")]
        public int? StudentId { get; set; }
        [Column("CourseId")]
        public int? CourseId { get; set; }
        [Column("LevelId")]
        public int? LevelId { get; set; }
        [Column("Identification")]
        public string? Identification { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
        [Column("Names")]
        public string? Names { get; set; }
    }
}

