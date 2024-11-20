using BUE.Inscriptions.Domain.Entity.DTO.reports;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class SingFileDTO
    {
        public string? Subject { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? SigningMethod { get; set; }
        public string? FileName { get; set; }
        public string? typeContract { get; set; }
        public int requestCode { get; set; }
        public byte[]? File { get; set; }
        public int? currentSchoolYear { get; set; }
        public bool? IsSend { get; set; }
        public bool? ExternalRead { get; set; }
        public bool? IsResend { get; set; }
        public SingBankDTO? Bank { get; set; }
    }
}
