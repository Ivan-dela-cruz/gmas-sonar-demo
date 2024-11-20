using BUE.Inscriptions.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Inscriptions.Entities
{
    [Table("Student")]
    public class Student:BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [ForeignKey("Company")]
        [Column("CompanyId")]
        public int CompanyId { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("MiddleName")]
        public string? MiddleName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("SecondLastName")]
        public string? SecondLastName { get; set; }

        [Column("IdentificationTypeId")]
        public int? IdentificationTypeId { get; set; }

        [Column("BirthCountryId")]
        public int? BirthCountryId { get; set; }

        [Column("NationalityId")]
        public int? NationalityId { get; set; }

        [Column("SecondaryNationalityId")]
        public int? SecondaryNationalityId { get; set; }

        [Column("GenderId")]
        public int? GenderId { get; set; }

        [Column("BloodTypeId")]
        public int? BloodTypeId { get; set; }

        [Column("Identification")]
        public string Identification { get; set; }

        [Column("BirthDate")]
        public DateTime? BirthDate { get; set; }

        [Column("Address")]
        public string? Address { get; set; }

        [Column("PreviousSchoolName")]
        public string? PreviousSchoolName { get; set; }

        [Column("BirthCity")]
        public string? BirthCity { get; set; }

        [Column("IsActive")]
        public bool? IsActive { get; set; }

        [Column("Image")]
        public string? Image { get; set; }

        [Column("DocumentIdentification")]
        public string? DocumentIdentification { get; set; }

        [Column("Sector")]
        public string? Sector { get; set; }

        [Column("Telephone")]
        public string? Telephone { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

        [Column("EmergencyContactPrefix")]
        public string? EmergencyContactPrefix { get; set; }

        [Column("EmergencyContact")]
        public string? EmergencyContact { get; set; }

        [Column("ExternalId")]
        public int? ExternalId { get; set; }

        [Column("Comment")]
        public string? Comment { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
        public virtual PaymentInformation? PaymentInformation { get; set; }
        public virtual List<StudentFamilies>? StudentFamilies { get; set; }
        public virtual List<Inscription>? Inscriptions { get; set; }

    }
}
