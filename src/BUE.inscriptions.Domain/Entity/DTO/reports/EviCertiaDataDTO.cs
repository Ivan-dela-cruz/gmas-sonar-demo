namespace BUE.Inscriptions.Domain.Entity.DTO.reports
{
    public class EviCertiaDataDTO
    {
        public string LookupKey { get; set; }

        public string Subject { get; set; }

        public byte[] Document { get; set; }

        public SigningPartieDTO SigningParties { get; set; }

        public OptionsDTO Options { get; set; }
    }
}
