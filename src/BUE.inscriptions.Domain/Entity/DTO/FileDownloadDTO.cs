using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class FileDownloadDTO
    {
        public int code { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public bool status { get; set; }
        public string identification { get; set; }
        public byte[]? file { get; set; }
        public string? langNames { get; set; }
        public string? urlFile { get; set; }
        public int? courseCode { get; set; }
        public LangFilesDTO? lang { get; set; }
        public string? Module { get; set; }
        public string? LangCode { get; set; }
    }
}
