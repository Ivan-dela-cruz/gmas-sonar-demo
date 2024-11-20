
namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
    public class TemplateReport
    {
        public int StudentId { get; set; }
        public int? AcademicYeartId { get; set; }
        public int? InscriptionId { get; set; }
        public string Client { get; set; }
        public string Template { get; set; }
        public bool? IsUpdate { get; set; }

    }
}
