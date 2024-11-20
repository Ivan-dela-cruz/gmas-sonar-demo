namespace BUE.Inscriptions.Domain.Entity.DTO.EviCertia
{
    public class ResponseQueryEviDTO
    {
        public List<EviQuerySingDTO> results { get; set; }

        public int totalMatches { get; set; }
    }
}
