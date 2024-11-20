using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Usuarios")]
    public class User
    {
        [Key]
        [Column("Codigo")]
        public int? Code { get; set; }
        [Column("PrimerApellido")]
        public string? FirstLastName { get; set; }
        [Column("SegundoApellido")]
        public string? SecondaryLastName { get; set; }
        [Column("FechaNacimiento")]
        public DateTime? BirthDate { get; set; }
        [Column("LugarNacimiento")]
        public string? BirthPlace { get; set; }
        [Column("Prefijo")]
        public string? Prefix { get; set; }
        [Column("EmailSecundario")]
        public string? SecondaryEmail { get; set; }
        [Column("CodigoContacto")]
        public int? ContactCode { get; set; }
        [Column("CodigoContactoSecundario")]
        public int? ContactCodeSecond { get; set; }
        [Column("CodigoIdioma")]
        public string? LanguagueCode { get; set; }
        [Column("Usuario")]
        public string? UserName { get; set; }
        [Column("Nombres")]
        public string? Names { get; set; }
        [Column("Identificacion")]
        public string? Identification { get; set; }
        [Column("Estado")]
        public bool? Status { get; set; }
        [Column("Activo")]
        public bool? Activo { get; set; }
        [Column("Imagen")]
        public string? Image { get; set; }
        [Column("verificacionEmail")]
        public bool? EmailVerification { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public byte[]? photo { get; set; }
        public string? RememberToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [Column("Direccion")]
        public string? Address { get; set; }
        [Column("TelefonoCelular")]
        public string? Cellphone { get; set; }
        [NotMapped]
        public string? PasswordHash { get; set; }
        [NotMapped]
        public string? PasswordSalt { get; set; }
        [NotMapped]
        public DateTime? TokenCreated { get; set; }
        [NotMapped]
        public DateTime? TokenExpires { get; set; }
        [NotMapped]
        public string? RefreshToken { get; set; } = string.Empty;
        public ICollection<UserRole> userRoles { get; set; }
        public virtual ContactPortal? Contact { get; set; }
        [Column("Referencia")]
        public int? Reference { get; set; }

    }
}
