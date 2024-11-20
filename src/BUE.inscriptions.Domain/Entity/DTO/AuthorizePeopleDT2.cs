using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class AuthorizePeopleDT2
    {
        public int? code { get; set; }
        public bool isRegisterPerson { get; set; }
        public int relationshipStudentCode { get; set; }
        public string address { get; set; }
        public string postalCode { get; set; }
        public string cellPhone { get; set; }
        public byte[]? photo { get; set; }
        public byte[]? documentFile { get; set; }
        public int typeIdentification { get; set; }
        public string documentNumber { get; set; }
        public int? studentCodeSchoolYearBue { get; set; }
        public int? studentCodeSchoolYear { get; set; }
        public int? currentSchoolYear { get; set; }
        public string? specifyRelationShip { get; set; }
    }
}
