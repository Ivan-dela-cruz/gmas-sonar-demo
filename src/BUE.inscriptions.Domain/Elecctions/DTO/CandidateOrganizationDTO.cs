using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Domain.Entity;


namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class CandidateOrganizationDTO
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public bool IsRepresentative { get; set; }
        public virtual Organization? Organization { get; set; }
        public virtual User? User { get; set; }
    }
}
