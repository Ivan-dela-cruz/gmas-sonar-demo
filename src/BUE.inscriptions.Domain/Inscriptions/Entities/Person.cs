using BUE.Inscriptions.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Inscriptions.Entities
{
    [Table("Person")]
    public class Person: BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [ForeignKey("Company")]
        [Column("CompanyId")]
        public int CompanyId { get; set; }
        [ForeignKey("User")]
        [Column("UserId")]
        public int UserId { get; set; }

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

        [Column("Identification")]
        public string Identification { get; set; }

        [Column("Nomination")]
        public string? Nomination { get; set; }

        [Column("BirthDate")]
        public DateTime? BirthDate { get; set; }

        [Column("BloodTypeId")]
        public int? BloodTypeId { get; set; }

        [Column("GenderId")]
        public int? GenderId { get; set; }

        [Column("BirthCountryId")]
        public int? BirthCountryId { get; set; }

        [Column("BirthCity")]
        public string? BirthCity { get; set; }

        [Column("IsActive")]
        public bool? IsActive { get; set; }

        [Column("NationalityId")]
        public int? NationalityId { get; set; }

        [Column("SecondaryNationalityId")]
        public int? SecondaryNationalityId { get; set; }

        [Column("MaritalStatus")]
        public int? MaritalStatus { get; set; }

        [Column("Image")]
        public string? Image { get; set; }

        [Column("DocumentIdentification")]
        public string? DocumentIdentification { get; set; }

        [Column("PostalId")]
        public string? PostalId { get; set; }

        [Column("Address")]
        public string? Address { get; set; }

        [Column("MainStreet")]
        public string? MainStreet { get; set; }

        [Column("SecondaryStreet")]
        public string? SecondaryStreet { get; set; }

        [Column("Sector")]
        public string? Sector { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

        [Column("HomePhone")]
        public string? HomePhone { get; set; }

        [Column("CellPhone")]
        public string? CellPhone { get; set; }

        [Column("CellPhonePrefix")]
        public string? CellPhonePrefix { get; set; }

        [Column("PhonePrefix")]
        public string? PhonePrefix { get; set; }

        [Column("ProfessionalSituation")]
        public int? ProfessionalSituation { get; set; }

        [Column("Position")]
        public string? Position { get; set; }

        [Column("OfficePhone")]
        public string? OfficePhone { get; set; }

        [Column("WorkAddress")]
        public string? WorkAddress { get; set; }

        [Column("WorkPlace")]
        public string? WorkPlace { get; set; }

        [Column("EmployerName")]
        public string? EmployerName { get; set; }

        [Column("WorkCountryId")]
        public int? WorkCountryId { get; set; }

        [Column("WorkCityId")]
        public int? WorkCityId { get; set; }

        [Column("WorkPhone")]
        public string? WorkPhone { get; set; }
        [Column("WorkCategory")]
        public int? WorkCategory { get; set; }

        [Column("WorkPhonePrefix")]
        public string? WorkPhonePrefix { get; set; }

        [Column("SystemAccess")]
        public bool? SystemAccess { get; set; }

        [Column("ShareContacts")]
        public bool? ShareContacts { get; set; }

        [Column("AdditionalInformation")]
        public string? AdditionalInformation { get; set; }
        [Column("ExternalId")]
        public int? ExternalId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
