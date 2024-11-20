namespace BUE.Inscriptions.Domain.Entity.DTO
{

    public class NotificationDTO
    {
        public int Code { get; set; }
        public string TemplateName { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Template { get; set; }
        public bool status { get; set; }
        public string Language { get; set; }
    }
}
