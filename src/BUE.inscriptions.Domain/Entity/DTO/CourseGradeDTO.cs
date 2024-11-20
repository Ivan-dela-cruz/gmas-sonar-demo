namespace BUE.Inscriptions.Domain.Entity.DTO
{

    public class CourseGradeDTO
    {
        public int Code { get; set; }
        public int levelCode { get; set; }
        public string Name { get; set; }
        public string? NameAlternative { get; set; }
        public bool Status { get; set; }
        public int? NextCourse { get; set; }
        public string? NameSecondLanguage { get; set; }
        public string? NameSecondLanguageShort { get; set; }
        public string? LevelSecondLanguage { get; set; }
        public virtual LevelDTO? level { get; set; }
    }
}
