namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class FinanceInformationDTO
    {
        public int code { get; set; }
        public int? checkbookCode { get; set; }
        public int? debitTypeCode { get; set; }
        public int? bankCode { get; set; }
        public int? contactCode { get; set; }
        public string? accountNumber { get; set; }
        public int typeIdentification { get; set; }
        public string? documentNumber { get; set; }
        public string? email { get; set; }
        public int studentCodeSchoolYear { get; set; }
        public int? studentCodeSchoolYearBue { get; set; }
        public int requestCode { get; set; }
        public string? lastName { get; set; }
        public string? secondName { get; set; }
        public string? names { get; set; }
        public int status { get; set; }
        public bool integrationStatus { get; set; }
        public string? namePaymentReference { get; set; }
        public int? paymentReference { get; set; }
        public int? paymentCode { get; set; }
        public int? companyScholarShip { get; set; }
        public bool isScholarShip { get; set; }
        public int? creditCardCode { get; set; }
        public virtual PortalRequest? PortalRequest { get; set; }
    }
}
