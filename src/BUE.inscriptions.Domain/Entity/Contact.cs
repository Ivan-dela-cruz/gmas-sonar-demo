using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Contacto")]
    public class Contact
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("CodigoContactoPadre")]
        public int? fatherCode { get; set; }
        [Column("Identificacion")]
        public string? documentNumber { get; set; }
        [Column("CodigoTipoIdentificacion")]
        public int? typeIdentification { get; set; }
        [Column("Nombres")]
        public string? names { get; set; }
        [Column("Apellidos")]
        public string? firstName { get; set; }
        [Column("NombreCompleto")]
        public string? completeName { get; set; }
        [Column("Direccion")]
        public string? address { get; set; }
        [Column("TelefonoCasa")]
        public string? landline { get; set; }
        [Column("TelefonoCelular")]
        public string? cellPhone { get; set; }
        [Column("TelefonoOficina")]
        public string? officePhone { get; set; }
        [Column("TelefonoExterior")]
        public string? externalPhone { get; set; }
        [Column("Email")]
        public string? email { get; set; }
        [Column("FechaNacimiento")]
        public DateTime? birthDate { get; set; }
        [Column("CodigoTipoSangre")]
        public int? typeBlood { get; set; }
        [Column("CodigoSexo")]
        public int? gender { get; set; }
        [Column("CodigoPaisNacimiento")]
        public int? birthCountry { get; set; }
        [Column("CiudadNacimiento")]
        public string? birthCity { get; set; }
        [Column("LugarTrabajo")]
        public string? workPlace { get; set; }
        [Column("DireccionTrabajo")]
        public string? workAddress { get; set; }
        [Column("Estado")]
        public bool? status { get; set; }
        [Column("Nominacion")]
        public string? prefix { get; set; }
        [Column("CodigoPais")]
        public int? countryResidence { get; set; }
        [Column("CodigoProvincia")]
        public int? cityResidence { get; set; }
        [Column("CodigoCanton")]
        public int? cantonCode { get; set; }
        [Column("CodigoParroquia")]
        public int? parishCode { get; set; }
        [Column("Foto")]
        public byte[]? photo { get; set; }
        [Column("CodigoNacionalidad")]
        public int? primaryNationality { get; set; }
        [Column("CodigoNacionalidad2")]
        public int? secondaryNationality { get; set; }
        [Column("CodigoNacionalidad3")]
        public int? thirtyNationality { get; set; }
        [Column("CodigoFamilia")]
        public string? familyCode { get; set; }
        [Column("Contrasena")]
        public string? password { get; set; }
        [Column("CodigoIdioma")]
        public int? language { get; set; }
        [Column("CodigoProfesion")]
        public int? professionCode { get; set; }
        [Column("EsPrimerLogin")]
        public bool? isFirstLogin { get; set; }
        [Column("IdiomaPortal")]
        public string? languagePortal { get; set; }
        [Column("ReseteoActivo")]
        public bool? resetActive { get; set; }
        [Column("prefijoCelular")]
        public string? prefixCellPhone { get; set; }
        [Column("prefijoTelefono")]
        public string? prefixLanline { get; set; }
        [Column("prefijoTelefonoTrabajo")]
        public string? prefixWorkPhone { get; set; }
        public List<StudentRepresentative> studentRepresentatives { get; set; }
    }
}
