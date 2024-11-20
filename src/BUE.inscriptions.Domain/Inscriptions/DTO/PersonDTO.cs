using BUE.Inscriptions.Domain.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public int? IdentificationTypeId { get; set; }
        public string Identification { get; set; }
        public string? Nomination { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? BloodTypeId { get; set; }
        public int? GenderId { get; set; }
        public int? BirthCountryId { get; set; }
        public string? BirthCity { get; set; }
        public bool? IsActive { get; set; }
        public int? NationalityId { get; set; }
        public int? SecondaryNationalityId { get; set; }
        public int? MaritalStatus { get; set; }
        public string? Image { get; set; }
        public string? DocumentIdentification { get; set; }
        public string? PostalId { get; set; }
        public string? Address { get; set; }
        public string? MainStreet { get; set; }
        public string? SecondaryStreet { get; set; }
        public string? Sector { get; set; }
        public string Email { get; set; }
        public string? HomePhone { get; set; }
        public string? CellPhone { get; set; }
        public string? CellPhonePrefix { get; set; }
        public string? PhonePrefix { get; set; }
        public int? ProfessionalSituation { get; set; }
        public string? Position { get; set; }
        public string? OfficePhone { get; set; }
        public string? WorkAddress { get; set; }
        public string? WorkPlace { get; set; }
        public string? EmployerName { get; set; }
        public int? WorkCountryId { get; set; }
        public int? WorkCityId { get; set; }
        public string? WorkPhone { get; set; }
        public int? WorkCategory { get; set; }
        public string? WorkPhonePrefix { get; set; }
        public bool? SystemAccess { get; set; }
        public bool? ShareContacts { get; set; }
        public string? AdditionalInformation { get; set; }
        public int? ExternalId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
        public virtual List<FileStorageDTO>? FilesStorage { get; set; }
    }
}
