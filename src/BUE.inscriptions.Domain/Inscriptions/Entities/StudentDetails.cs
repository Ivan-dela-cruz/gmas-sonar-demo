
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Inscriptions.Entities
{
    public class StudentDetails
    {
        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentIdentification { get; set; }
        public string? StudentImage { get; set; }
        public string? StudentAddress { get; set; }
        public string? StudentSector { get; set; }
        public string? StudentBirthCity { get; set; }
        public DateTime? StudentBirthDate { get; set; }
        public string? StudentMartitalStatus { get; set; }
        public string? StudentCanton { get; set; }
        public string? StudentParish { get; set; }
        public string? StudentEmail { get; set; }
        public string? StudentPhone { get; set; }
        public string? StudentTypeIdentification { get; set; }
        public string? StudentCourse { get; set; }
        public string? StudentLevel { get; set; }
        public string? StudentNatinality { get; set; }
        public int? StudentExternalId { get; set; }
        public int? PrincipalPersonId { get; set; }
        public string? PrincipalPersonName { get; set; }
        public string? PrincipalPersonIdentification { get; set; }
        public string? PrincipalPersonAddress { get; set; }
        public string? PrincipalPersonSector { get; set; }
        public string? PrincipalPersonBirthCity { get; set; }
        public DateTime? PrincipalPersonBirthDate { get; set; }
        public string? PrincipalPersonMartitalStatus { get; set; }
        public string? PrincipalPersonEmail { get; set; }
        public string? PrincipalPersonPhone { get; set; }
        public string? PrincipalPersonNatinality { get; set; }
        public string? PrincipalPersonPosition { get; set; }
        public string? PrincipalPersonWorkPlace { get; set; }
        public string? PrincipalPersonWorkPhone { get; set; }
        public string? PrincipalPersonWorkAddress { get; set; }
        public string? PrincipalPersonRelationType { get; set; }
        public string? PrincipalPersonProfessionalSituation { get; set; }
        public int? SecondPersonId { get; set; }
        public string? SecondPersonName { get; set; }
        public string? SecondPersonIdentification { get; set; }
        public string? SecondPersonAddress { get; set; }
        public string? SecondPersonSector { get; set; }
        public string? SecondPersonBirthCity { get; set; }
        public DateTime? SecondPersonBirthDate { get; set; }
        public string? SecondPersonMartitalStatus { get; set; }
        public string? SecondPersonEmail { get; set; }
        public string? SecondPersonPhone { get; set; }
        public string? SecondPersonNatinality { get; set; }
        public string? SecondPersonPosition { get; set; }
        public string? SecondPersonWorkPlace { get; set; }
        public string? SecondPersonWorkPhone { get; set; }
        public string? SecondPersonWorkAddress { get; set; }
        public string? SecondPersonRelationType { get; set; }
        public string? SecondPersonProfessionalSituation { get; set; }
        public DateTime? InscriptionCreatedAt { get; set; }
        public DateTime? InscriptionEnrollmentDate { get; set; }
        public int? InscriptionId { get; set; }
        public string? InscriptionAcademicYear { get; set; }
        public string? InscriptionUseTransport { get; set; }
        public string? InscriptionAuthorizeImageUsage { get; set; }
        public string? InscriptionAuthorizeImagePublication { get; set; }
        public string? InscriptionInstitutionalRepresentation { get; set; }
        public string? InscriptionPedagogicalTrip { get; set; }
    }

}
