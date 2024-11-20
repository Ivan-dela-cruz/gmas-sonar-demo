namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class ApplicationDTO
    {

        public int code { get; set; }
        public int? currentSchoolYear { get; set; }
        public int? afterSchoolYear { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool? Status { get; set; }
        public bool? isEnrollment { get; set; }
        public byte[]? photo { get; set; }
        public string? urlImage { get; set; }
        public string? ruc { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? cellPhone { get; set; }
        public string? description { get; set; }
        public string? additionalInformation { get; set; }
        public virtual PortalSchoolYear? SchoolYear { get; set; }
    }
}
