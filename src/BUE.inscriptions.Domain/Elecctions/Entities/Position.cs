using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BUE.Inscriptions.Domain.Entity;

namespace BUE.Inscriptions.Domain.Elecctions.Entities
{
    [Table("Positions")]
    public class Position : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("image")]
        public string? Image { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [Column("abbreviation")]
        public string? Abbreviation { get; set; }

    }
}
