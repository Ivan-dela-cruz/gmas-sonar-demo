
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class UserDTO
    {
        public int? Code { get; set; }
        public int? ContactCode { get; set; }
        public int? ContactCodeSecond { get; set; }
        public string LanguagueCode { get; set; }
        public string? UserName { get; set; }
        public string? Names { get; set; }
        public string? Identification { get; set; }
        public bool? Status { get; set; }
        public bool? Activo { get; set; }
        public string? Image { get; set; }
        public bool? EmailVerification { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public byte[]? photo { get; set; }
        public string? RememberToken { get; set; }
        public string? FirstLastName { get; set; }
        public string? SecondaryLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? Prefix { get; set; }
        public string? SecondaryEmail { get; set; }
        public string? Address { get; set; }
        public string? Cellphone { get; set; }
        public int? RoleId { get; set; }
        public int? Reference { get; set; }
        // [JsonIgnore]
        public IEnumerable<RoleDTO>? roles { get; set; }
        public ContactPortalDTO? Contact { get; set; }
    }
}

