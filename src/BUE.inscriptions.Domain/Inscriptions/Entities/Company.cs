using BUE.Inscriptions.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Inscriptions.Entities
{
    [Table("Company")]
    public class Company:BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("CompanyName")]
        public string CompanyName { get; set; }

        [Column("Industry")]
        public string Industry { get; set; }

        [Column("Website")]
        public string Website { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("AddressLine1")]
        public string AddressLine1 { get; set; }

        [Column("AddressLine2")]
        public string AddressLine2 { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("State")]
        public string State { get; set; }

        [Column("Country")]
        public string Country { get; set; }

        [Column("PostalCode")]
        public string PostalCode { get; set; }

        [Column("IsActive")]
        public bool? IsActive { get; set; }
    }
}
