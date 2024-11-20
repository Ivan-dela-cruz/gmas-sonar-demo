using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Domain.Response
{
    public class CurrentUserResponse
    {
        public int? Code { get; set; }
        public int? ContactCode { get; set; }
        public int? ContactCodeSecond { get; set; }
        public string LanguagueCode { get; set; }
        public string? UserName { get; set; }
        public string Names { get; set; }
        public string Identification { get; set; }
        public bool? Status { get; set; }
        public bool? Activo { get; set; }
        public string? Image { get; set; }
        public bool? EmailVerification { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string FirstLastName { get; set; }
        public string? SecondaryLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? Prefix { get; set; }
        public string? SecondaryEmail { get; set; }
        public ApplicationDTO? application { get; set; }
        public IEnumerable<RoleDTO>? roles { get; set; }
        public SchoolYearDTO? CurrentSchoolYear { get; set; }

    }
}
