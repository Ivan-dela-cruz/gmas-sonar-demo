using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("InformacionFinanciera")]
    public class FinanceInformation : BaseEntity
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("CodigoChequera")]
        public int? checkbookCode { get; set; }
        [Column("CodigoTipoDebito")]
        public int? debitTypeCode { get; set; }
        [Column("CodigoBanco")]
        public int? bankCode { get; set; }
        [Column("CodigoContacto")]
        public int contactCode { get; set; }
        [Column("NumeroCuenta")]
        public string? accountNumber { get; set; }
        [Column("CodigoTipoIdentificacion")]
        public int typeIdentification { get; set; }
        [Column("Identificacion")]
        public string? documentNumber { get; set; }
        [Column("NombreReferenciaPago")]
        public string? namePaymentReference { get; set; }
        [Column("EmailNotificacion")]
        public string? email { get; set; }
        [Column("CodigoEstudianteAnioLectivo")]
        public int studentCodeSchoolYear { get; set; }
        [Column("CodigoEstudianteAnioLectivoBue")]
        public int? studentCodeSchoolYearBue { get; set; }
        [Column("CodigoSolicitud")]
        public int requestCode { get; set; }
        [Column("PrimerApellido")]
        public string? lastName { get; set; }
        [Column("SegundoApellido")]
        public string? secondName { get; set; }
        [Column("Nombres")]
        public string? names { get; set; }
        [Column("Estado")]
        public int status { get; set; }
        [Column("EstadoIntegracion")]
        public bool integrationStatus { get; set; }

        [Column("referenciaPago")]
        public int? paymentReference { get; set; }
        [Column("CodigoFormaPago")]
        public int? paymentCode { get; set; }
        [Column("CompaniaBeca")]
        public int? companyScholarShip { get; set; }
        [Column("CodigoTarjetaCredito")]
        public int? creditCardCode { get; set; }
        [Column("EsBecado")]
        public bool isScholarShip { get; set; }
        public virtual PortalRequest? PortalRequest { get; set; }
    }
}
