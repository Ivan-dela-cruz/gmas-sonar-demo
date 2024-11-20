using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("PersonasAutorizadas")]
    public class AuthorizePeople : BaseEntity
    {
        [Key]
        [Column("Codigo")]
        public int? code { get; set; }
        [Column("CodigoSolicitud")]
        public int? portalRequestCode { get; set; }
        [Column("AutorizaRetiro")]
        public bool? isRegisterPerson { get; set; }
        [Column("Parentesco")]
        public int? relationshipStudentCode { get; set; }
        [Column("Direccion")]
        public string? address { get; set; }
        [Column("CodigoPostal")]
        public string? postalCode { get; set; }
        [Column("TelefonoCelular")]
        public string? cellPhone { get; set; }
        [Column("Foto")]
        public byte[]? photo { get; set; }
        [Column("DocumentoIdendificacion")]
        public byte[]? documentFile { get; set; }
        [Column("CodigoTipoIdentificacion")]
        public int? typeIdentification { get; set; }
        [Column("Identificacion")]
        public string? documentNumber { get; set; }
        [Column("CodigoEstudianteAnioLectivoBue")]
        public int? studentCodeSchoolYearBue { get; set; }
        [Column("CodigoEstudianteAnioLectivo")]
        public int? studentCodeSchoolYear { get; set; }
        [Column("CodigoAnioLectivo")]
        public int? currentSchoolYear { get; set; }
        [Column("UrlImage")]
        public string? urlImage { get; set; }
        [Column("UrlDocument")]
        public string? urlDocument { get; set; }
        [Column("Nombres")]
        public string? names { get; set; }
        [Column("ApellidoPaterno")]
        public string? firstName { get; set; }
        [Column("ApellidoMaterno")]
        public string? secondName { get; set; }
        [Column("Email")]
        public string? email { get; set; }
        [Column("CodigoContactoBue")]
        public int? contactCodeBue { get; set; }
        [Column("EstadoIntegracion")]
        public int? statusIntegration { get; set; }
        [Column("EspecifiqueParentesco")]
        public string? specifyRelationShip { get; set; }
        public PortalRequest? PortalRequest { get; set; }
    }
}