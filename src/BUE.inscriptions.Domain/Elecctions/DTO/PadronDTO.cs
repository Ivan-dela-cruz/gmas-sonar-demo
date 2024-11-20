namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class PadronDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ParentId { get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        public int? LevelId { get; set; }
        public string? Identification { get; set; }
        public string? Email { get; set; }
        public string? Names { get; set; }
    }
}
