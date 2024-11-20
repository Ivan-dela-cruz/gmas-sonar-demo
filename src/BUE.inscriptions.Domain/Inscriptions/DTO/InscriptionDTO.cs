using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using System.ComponentModel.DataAnnotations;
namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
    public class InscriptionDTO
    {
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int? AcademicYearId { get; set; }
        public int? PhotographyStatus { get; set; }
        public int? RequestStatus { get; set; }
        public bool? IsActive { get; set; }
        public string? FileUrl { get; set; }
        public string? Comment1 { get; set; }
        public string? Comment2 { get; set; }
        public string? Comment3 { get; set; }
        public string? StudentRepresentativeInfo { get; set; }
        public string? IntegrationInformation { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? Notes { get; set; }
        public int? SecondRepresentativeReason { get; set; }
        public bool? SecondRepresentativeRegistration { get; set; }
        public string? SingServiceId { get; set; }
        public string? SingServiceUrl { get; set; }
        public bool? SingServiceStatus { get; set; }
        public string? SingTransportId { get; set; }
        public string? SingTransportUrl { get; set; }
        public bool? SingTransportStatus { get; set; }
        public string? BankSingServiceId { get; set; }
        public string? BankSingServiceUrl { get; set; }
        public bool? BankSingServiceStatus { get; set; }
        public int? PreviousStatus { get; set; }
        public string? EnrollmentProcess { get; set; }
        public string? SecondRepresentativeJustificationUrl { get; set; }
        public string? CompleteReportUrl { get; set; }
        public int? PreviousGradeCourseId { get; set; }
        public int? LevelId { get; set; }
        public int? GradeCourseId { get; set; }
        public int? ParallelId { get; set; }
        public int? SpecializationId { get; set; }
        public bool? IsRepeater { get; set; }
        public bool? EnrollmentProcessStatus { get; set; }
        public bool? CompleteDocumentation { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public bool? Withdrawn { get; set; }
        public DateTime? WithdrawalDate { get; set; }
        public string? WithdrawalDestination { get; set; }
        public bool? InternationalSection { get; set; }
        public int? EnrollmentType { get; set; }
        public bool? AcceptConditions { get; set; }
        public bool? AuthorizeImageUsage { get; set; }
        public bool? AuthorizeImagePublication { get; set; }
        public bool? AuthorizeImageSharing { get; set; }
        public bool? AuthorizeImageRetention { get; set; }
        public int? PreviousEstablishmentCountryId { get; set; }
        public int? PreviousEstablishmentCityId { get; set; }
        public bool? IsScholarshipRecipient { get; set; }
        public int? ScholarshipOrganism { get; set; }
        public bool? UseTransport { get; set; }
        public bool? AcceptTransportConditions { get; set; }
        public bool? AuthorizeDeparture { get; set; }
        public int? EmergencyReference { get; set; }
        public bool? PedagogicalTrip { get; set; }
        public bool? InstitutionalRepresentation { get; set; }
        public bool? AcceptTerms { get; set; }
        public bool? AcceptUseData { get; set; }
        public bool? InstitutionQuestion { get; set; }
        public bool? WithdrawalQuestion { get; set; }
        public bool? NotAuthorizedLeaveAlone { get; set; }
        public bool? AuthorizedFreeHoursDeparture { get; set; }
        public int? ExternalId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual AcademicYear? AcademicYear { get; set; }
        public virtual User? User { get; set; }
        public virtual LevelPortal? Level { get; set; }
        public virtual CourseGradePortal? CourseGrade { get; set; }


    }
}
