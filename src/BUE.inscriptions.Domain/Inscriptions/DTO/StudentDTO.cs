using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public int? IdentificationTypeId { get; set; }
        public int? BirthCountryId { get; set; }
        public int? NationalityId { get; set; }
        public int? SecondaryNationalityId { get; set; }
        public int? GenderId { get; set; }
        public int? BloodTypeId { get; set; }
        public string Identification { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? PreviousSchoolName { get; set; }
        public string? BirthCity { get; set; }
        public bool? IsActive { get; set; }
        public string? Image { get; set; }
        public string? DocumentIdentification { get; set; }
        public string? Sector { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? EmergencyContactPrefix { get; set; }
        public string? EmergencyContact { get; set; }
        public int? ExternalId { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual List<FileStorageDTO>? FilesStorage { get; set; }
        public virtual Company? Company { get; set; }
        public virtual PaymentInformation? PaymentInformation { get; set; }
        public virtual List<StudentFamiliesDTO>? StudentFamilies { get; set; }
        public virtual List<InscriptionDTO>? Inscriptions { get; set; }
    }
}
