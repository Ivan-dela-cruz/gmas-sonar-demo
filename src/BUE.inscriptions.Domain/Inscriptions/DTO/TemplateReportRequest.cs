
namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
    public class TemplateReportRequest<T>
    {
        public string Client { get; set; }
        public string Template { get; set; }
        public T? Data { get; set; }

    }
}
