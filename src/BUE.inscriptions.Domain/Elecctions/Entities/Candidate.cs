using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BUE.Inscriptions.Domain.Entity;

namespace BUE.Inscriptions.Domain.Elecctions.Entities
{
    [Table("Candidates")]
    public class Candidate : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("second_name")]
        public string? SecondName { get; set; }

        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("second_lastname")]
        public string? SecondLastName { get; set; }

        [Column("entry_date")]
        public DateTime? EntryDate { get; set; }

        [Column("exit_date")]
        public DateTime? ExitDate { get; set; }

        [Column("identification")]
        public string? Identification { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone1")]
        public string? Phone1 { get; set; }

        [Column("phone2")]
        public string? Phone2 { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [Column("abbreviation")]
        public string? Abbreviation { get; set; }
        [Column("image")]
        public string? Image { get; set; }
    }
}
