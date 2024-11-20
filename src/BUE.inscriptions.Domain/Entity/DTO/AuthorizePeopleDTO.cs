

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class AuthorizePeopleDTO
    {
        public int? code { get; set; }
        public int? portalRequestCode { get; set; }
        public bool? isRegisterPerson { get; set; }
        public int? relationshipStudentCode { get; set; }
        public string? address { get; set; }
        public string? postalCode { get; set; }
        public string? cellPhone { get; set; }
        public byte[]? photo { get; set; }
        public byte[]? documentFile { get; set; }
        public int? typeIdentification { get; set; }
        public string? documentNumber { get; set; }
        public int? studentCodeSchoolYearBue { get; set; }
        public int? studentCodeSchoolYear { get; set; }
        public int? currentSchoolYear { get; set; }
        public string? urlImage { get; set; }
        public string? urlDocument { get; set; }
        public string? names { get; set; }
        public string? firstName { get; set; }
        public string? secondName { get; set; }
        public string? email { get; set; }
        public int? contactCodeBue { get; set; }
        public int? statusIntegration { get; set; }
        public string? specifyRelationShip { get; set; }
    }
}
