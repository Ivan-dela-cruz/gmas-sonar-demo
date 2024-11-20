using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BUE.Inscriptions.Domain.Entity;

namespace BUE.Inscriptions.Domain.Elecctions.Entities
{
    [Table("Organizations")]
    public class Organization : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("image")]
        public string? Image { get; set; }
        [Column("banner_image")]
        public string? BannerImage { get; set; } 
        [Column("additional_files")]
        public string? AdditionalFiles { get; set; }
        [Column("acronym")]
        public string? Acronym { get; set; }
        [Column("slogan")]
        public string? Slogan { get; set; }
        [Column("content")]
        public string? Content { get; set; } 
        [Column("proposal")]
        public string? Proposal { get; set; }
        [Column("status")]
        public bool Status { get; set; }
    }
}
