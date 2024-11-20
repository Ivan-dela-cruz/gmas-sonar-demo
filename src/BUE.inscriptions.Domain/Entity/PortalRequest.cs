using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Solicitudes")]
    public class PortalRequest : BaseEntity
    {
        [Column("Codigo")]
        [Key]
        public int? code { get; set; }
        [Column("CodigoUsuario")]
        public int? userCode { get; set; }
        [Column("CodigoAnioLectivo")]
        public int? currentSchoolYear { get; set; }
        [Column("CodigoEstudianteAnioLectivo")]
        [ForeignKey("Student")]
        public int? studentCodeSchoolYear { get; set; }
        [Column("CodigoPrimerRepresentante")]
        public int? contactCodeFirst { get; set; }
        [Column("CodigoSegundoRepresentante")]
        public int? contactCodeSecond { get; set; }
        [Column("EstadoFotografia")]
        public int? photoStatus { get; set; }
        [Column("EstadoSolicitud")]
        public int? requestStatus { get; set; }
        [Column("Estado")]
        public bool? status { get; set; }
        [Column("urlFile")]
        public string? urlFile { get; set; }
        [Column("Coment1")]
        public string? comment1 { get; set; }
        [Column("Coment2")]
        public string? comment2 { get; set; }
        [Column("Coment3")]
        public string? comment3 { get; set; }
        [Column("Coment4")]
        public string? comment4 { get; set; }
        [Column("Coment5")]
        public string? comment5 { get; set; }
        [Column("InformacionAdicional")]
        public string? additionalInformation { get; set; }
        [Column("Notas")]
        public string? notes { get; set; }
        [Column("EstadoPrimerRepresentate")]
        public int? statusFirstContact { get; set; }
        [Column("EstadoSegundoRepresentate")]
        public int? statusSecondContact { get; set; }
        [Column("MotivoRepresentante2")]
        public int? reasonsRpt { get; set; }
        [Column("RegistroRepresentante2")]
        public bool? registerRepresentative { get; set; }
        public string? IdSingService { get; set; }

        public string? UrlSingService { get; set; }

        public bool? statusSingService { get; set; }

        public string? IdSingTransport { get; set; }

        public string? UrlSingTransport { get; set; }

        public bool? statusSingTransport { get; set; }

        public string? IdBankSingService { get; set; }

        public string? UrlBankSingService { get; set; }

        public bool? statusBankSingService { get; set; }

        [Column("AceptoTerminos")]
        public bool? AceptTerms { get; set; }
        [Column("EstadoAnterior")]
        public int? requestBeforeStatus { get; set; }
        [Column("procesoInscripcion")]
        public string? dataEnrollment { get; set; }
        [Column("urlJustificacionRepresentante2")]
        public string? UrlFileJustification { get; set; }
        [Column("urlReportComplete")]
        public string? UrlReportComplete { get; set; }

        public virtual StudentPortal? StudentPortal { get; set; }
        public virtual FinanceInformation? FinanceInformation { get; set; }
        public virtual MedicalRecord? MedicalRecord { get; set; }
        public virtual ContactPortal? FirstContact { get; set; }
        public virtual ContactPortal? SecondContact { get; set; }
        public virtual List<AuthorizePeople>? AuthorizePeople { get; set; }
        [NotMapped]
        public virtual PortalSchoolYear? PortalSchoolYear { get; set; }

    }
}
