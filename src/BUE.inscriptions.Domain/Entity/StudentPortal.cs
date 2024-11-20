﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("EstudianteAnioLectivoPM")]
    public class StudentPortal
    {
        [Key]
        [Column("CodigoEstudianteAnioLectivo")]
        public int? studentCodeSchoolYear { get; set; }
        [Column("CodigoEstudianteAnioLectivoBue")]
        public int? studentCodeSchoolYearBue { get; set; }
        [Column("CodigoEstudiante")]
        public int? studentCode { get; set; }
        [Column("CodigoTipoIdentificacion")]
        public int? typeIdentification { get; set; }
        [Column("CodigoPaisNacimiento")]
        public int? birthCountry { get; set; }
        [Column("CodigoNacionalidad")]
        public int? primaryNationality { get; set; }
        [Column("CodigoNacionalidad2")]
        public int? secondaryNationality { get; set; }
        [Column("CodigoNacionalidad3")]
        public int? thirtyNationality { get; set; }
        [Column("CodigoPaisResidencia")]
        public int? CodigoPaisResidencia { get; set; }
        [Column("CodigoProvinciaResidencia")]
        public int? CodigoProvinciaResidencia { get; set; }
        [Column("CodigoCantonResidencia")]
        public int? CodigoCantonResidencia { get; set; }
        [Column("CodigoParroquiaResidencia")]
        public int? CodigoParroquiaResidencia { get; set; }
        [Column("CodigoSexo")]
        public int? gender { get; set; }
        [Column("CodigoTipoSangre")]
        public int? BloodTypeCode { get; set; }
        [Column("CodigoAnioLectivo")]
        public int? currentSchoolYear { get; set; }
        [Column("CodigoNivel")]
        public int? levelCode { get; set; }
        [Column("CodigoCursoGrado")]
        public int? courseGradeCode { get; set; }
        [Column("codigoCursoGradoAnterior")]
        public int? BeforeCourseGradeCode { get; set; }
        [Column("CodigoParalelo")]
        public int? parallelCode { get; set; }
        [Column("CodigoEspecialidad")]
        public int? specialtyCode { get; set; }
        [Column("CodigoEstadoCivilPadres")]
        public int? civilStatus { get; set; }
        [Column("CodigoIdioma")]
        public int? language { get; set; }
        [Column("Identificacion")]
        public string? documentNumber { get; set; }
        [Column("Nombres")]
        public string? names { get; set; }
        [Column("Apellidos")]
        public string? firstName { get; set; }
        [Column("NombreCompleto")]
        public string? completeName { get; set; }
        [Column("FechaNacimiento")]
        public DateTime? birthDate { get; set; }
        [Column("PrimerApellidoPaterno")]
        public string? fatherLastName { get; set; }
        [Column("NombreUsual")]
        public string? usualName { get; set; }
        [Column("Direccion")]
        public string? primaryResidence { get; set; }
        [Column("FechaIngreso")]
        public DateTime? dateAdmission { get; set; }
        [Column("NombreColegioProcede")]
        public string? previusCollege { get; set; }
        [Column("CiudadNacimiento")]
        public string? birthCity { get; set; }
        [Column("Estado")]
        public bool? status { get; set; }
        [Column("EstadoAnioLectivo")]
        public bool? statusSchoolYear { get; set; }
        [Column("CodigoFamilia")]
        public string? familyCode { get; set; }
        [Column("EsRepetidor")]
        public bool? isRepeatClass { get; set; }
        [Column("ProcesoMatricula")]
        public bool? isEnrollmentProcess { get; set; }
        [Column("DocumentacionCompleta")]
        public bool? isFullDocumentation { get; set; }
        [Column("FechaMatricula")]
        public DateTime? enrollmentDate { get; set; }
        [Column("AutorizaSalidaMedioDia")]
        public bool? AutorizaSalidaMedioDia { get; set; }
        [Column("AutorizaSalidaTarde")]
        public bool? AutorizaSalidaTarde { get; set; }
        [Column("Retirado")]
        public bool? Retirado { get; set; }
        [Column("FechaRetiro")]
        public DateTime? FechaRetiro { get; set; }
        [Column("DestinoRetiro")]
        public string? DestinoRetiro { get; set; }
        [Column("FranciaRetiro")]
        public bool? FranciaRetiro { get; set; }
        [Column("FechaActualizacion")]
        public DateTime? FechaActualizacion { get; set; }
        [Column("UsuarioActualizacion")]
        public string? UsuarioActualizacion { get; set; }
        [Column("CalcularInteres")]
        public bool? CalcularInteres { get; set; }
        [Column("SeccionInternacional")]
        public bool? SeccionInternacional { get; set; }
        //[Column("Foto")]
        //public byte[]? photo { get; set; }
        [Column("SegundoApellidoMaterno")]
        public string? secondName { get; set; }
        //[Column("DocumentoIdendificacion")]
        //public byte[]? documentIdentification { get; set; }
        [Column("ProcesoMatriculaAplicacion")]
        public string? dataEnrollmentApp { get; set; }
        [Column("TipoInscripcion")]
        public int? firstInscription { get; set; }
        [Column("AceptoCondiciones")]
        public bool? aceptedConditions { get; set; }
        [Column("AutorizaUsarImagen")]
        public bool? isUseAuthorization { get; set; }
        [Column("AutorizaPublicarImagen")]
        public bool? isPublication { get; set; }
        [Column("AutorizaCompartirImagen")]
        public bool? isComunication { get; set; }
        [Column("AutorizaConservarImagen")]
        public bool? isConservation { get; set; }

        [Column("CodigoPaisEstablecimientoAnterior")]
        public int? countryAfterClass { get; set; }
        [Column("CodigoCiudadEstablecimientoAnterior")]
        public int? cityAfterClass { get; set; }
        [Column("EsAEFE")]
        public bool? isAEFE { get; set; }
        [Column("PagoReferencia")]
        public int? paymentReference { get; set; }
        [Column("NombreReferenciaPago")]
        public string? namePaymentReference { get; set; }
        [Column("EsBecado")]
        public bool? isScholarShip { get; set; }
        [Column("OrganismoBeca")]
        public int? companyScholarShip { get; set; }
        [Column("TomarTransporte")]
        public bool? isSchoolBus { get; set; }
        [Column("AceptoCondicionesTransporte")]
        public bool? isConditionsSchoolBus { get; set; }
        [Column("AutorizaSalida")]
        public bool? isAuthExitSchool { get; set; }
        [Column("EmergenciaReferencia")]
        public int? actionEmergency { get; set; }
        [Column("EstadoProcesoMatricula")]
        public string? enrollmentProcessStatus { get; set; }
        [Column("UrlImagen")]
        public string? urlImage { get; set; }
        [Column("UrlDocument")]
        public string? urlDocument { get; set; }
        [Column("locationBeforeCollege")]
        public string? locationBeforeCollege { get; set; }
        [Column("salidaPedagogica")]
        public bool? pedagogicalOutput { get; set; }
        [Column("representacionInstitucion")]
        public bool? schoolRepresentation { get; set; }
        [Column("Sector")]
        public string? Sector { get; set; }
        [Column("Telefono")]
        public string? Phone { get; set; }
        [Column("Email")]
        public string? Email { get; set; } 
        [Column("PrefijoContactoEmergencia")]
        public string? PrefixEmergencyContact { get; set; }  
        [Column("ContactoEmergencia")]
        public string? EmergencyContact { get; set; }
        [Column("AceptoTerminos")]
        public bool? AceptTerms { get; set; }
        [Column("PreguntaInstitucion")]
        public bool? InstitutionQuestion { get; set; } 
        [Column("PreguntaRetiro")]
        public bool? WithdrawalQuestion { get; set; }  
        [Column("NoAutorizadoSalirSolo")]
        public bool? NotAuthorizedAlone { get; set; }
        [Column("AutorizacionSalidaHorasLibres")]
        public bool? FreeHoursExitAuthorization { get; set; }
        public bool? aceptUseData { get; set; }
        public virtual LevelPortal? level { get; set; }
        public virtual CourseGradePortal? courseGrade { get; set; }
        public virtual PortalRequest? PortalRequest { get; set; }
    }
}