namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class MailNotificactionDTO
    {
        public string emails { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string? userCode { get; set; }
        public string? contactCode { get; set; }
        public string? studentCode { get; set; }
        public string? student { get; set; }
        public string? course { get; set; }
        public string? schoolYear { get; set; }
        public string? lang { get; set; }
        public string? template { get; set; }
        public string? requestId { get; set; }
    }
}
