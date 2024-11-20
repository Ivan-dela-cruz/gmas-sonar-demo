namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class StudentRepresentativeDTO
    {
        public int code { get; set; }
        public int studentCodeSchoolYear { get; set; }
        public int contactCode { get; set; }
        public bool isLegalRepresentative { get; set; }
        public bool isResponsible { get; set; }
        public int relationshipStudentCode { get; set; }
        public int eventCode { get; set; }
        public DateTime? updatedAt { get; set; }
        public string? userUpdate { get; set; }
        public virtual IEnumerable<StudentBUEDTO> students { get; set; }
    }
}
