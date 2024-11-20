using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using System.ComponentModel.DataAnnotations;


namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class OrganizationDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 100 characters")]
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? BannerImage { get; set; }
        public string? AdditionalFiles { get; set; }
        [Required(ErrorMessage = "The Acronym is required")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "The Acronym must be between 3 and 25 characters")]
        public string? Acronym { get; set; }
        public string? Slogan { get; set; }
        public string? Content { get; set; }
        public string? Proposal { get; set; }
        public bool Status { get; set; }
        public virtual List<FileStorageDTO>? FilesStorage { get; set; }
        public virtual IEnumerable<CandidateOrganizationDTO>? CandidateOrganizations { get; set; }
    }
}
