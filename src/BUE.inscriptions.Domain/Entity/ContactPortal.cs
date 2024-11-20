using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("ContactoPM")]
    public class ContactPortal : BaseEntity
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
        [Column("SegundoApellidoMaterno")]
        public string? secondName { get; set; }
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
        //[Column("Foto")]
        //public byte[]? photo { get; set; }
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



        [Column("EsRepresentanteLegal")]
        public bool? isLegalRepresentative { get; set; }
        [Column("CodigoParentesco")]
        public int? relationshipStudentCode { get; set; }
        [Column("CodigoParentesco2")]
        public int? relationshipOther { get; set; }
        [Column("SituacionProfesional")]
        public int? professionalSituation { get; set; }
        [Column("NombreEmpleador")]
        public string? nameEmployer { get; set; }
        [Column("CodigoPaisTrabajo")]
        public int? countryWork { get; set; }
        [Column("CodigoCiudadTrabajo")]
        public int? cityWork { get; set; }
        [Column("TelefonoTrabajo")]
        public string? phoneWork { get; set; }
        [Column("CategoriaTrabajo")]
        public int? categoryWork { get; set; }
        [Column("cargo")]
        public string? positionWork { get; set; }
        [Column("CompartirContactos")]
        public bool? shareContacts { get; set; }
        //[Column("DocumentoIdendificacion")]
        //public byte[]? documentFile { get; set; }
        [Column("codigoContactoBue")]
        public int? ContactCodeBue { get; set; }
        [Column("TipoRepresentante")]
        public int? typeRepresentative { get; set; }
        [Column("InformacionAdicional")]
        public string? additionalInformation { get; set; }
        [Column("CodigoEstudianteAnioLectivo")]
        public int? studentCodeSchoolYear { get; set; }
        [Column("CodigoAnioLectivo")]
        public int? currentSchoolYear { get; set; }
        [Column("EstadoCivil")]
        public int? civilStatus { get; set; }

        [Column("MotivoRepresentante2")]
        public int? reasonsRpt { get; set; }
        [Column("UrlImage")]
        public string? urlImage { get; set; }
        [Column("UrlDocument")]
        public string? urlDocument { get; set; }
        [Column("CodigoPostal")]
        public string? postalCode { get; set; }
        [Column("locationResidence")]
        public string? locationResidence { get; set; }
        [Column("locationWork")]
        public string? locationWork { get; set; }
        [Column("CodigoUsuario")]
        public int? userCode { get; set; }
        [Column("prefijoCelular")]
        public string? prefixCellPhone { get; set; }
        [Column("prefijoTelefono")]
        public string? prefixLanline { get; set; }
        [Column("prefijoTelefonoTrabajo")]
        public string? prefixWorkPhone { get; set; }
        public virtual List<PortalRequest>? PortalRequest { get; set; }
        public virtual List<PortalRequest>? SecondRequest { get; set; }

    }
}
