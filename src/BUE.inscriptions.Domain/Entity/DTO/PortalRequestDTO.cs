namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class PortalRequestDTO
    {
        public int? code { get; set; }
        public int? userCode { get; set; }
        public int? currentSchoolYear { get; set; }
        public int? studentCodeSchoolYear { get; set; }
        public int? contactCodeFirst { get; set; }
        public int? contactCodeSecond { get; set; }
        public int? photoStatus { get; set; }
        public int? requestStatus { get; set; }
        public bool? status { get; set; }
        public string? urlFile { get; set; }
        public string? comment1 { get; set; }
        public string? comment2 { get; set; }
        public string? comment3 { get; set; }
        public string? comment4 { get; set; }
        public string? comment5 { get; set; }
        public string? additionalInformation { get; set; }
        public string? notes { get; set; }
        public int? statusFirstContact { get; set; }
        public int? statusSecondContact { get; set; }
        public int? reasonsRpt { get; set; }
        public bool? registerRepresentative { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? IdSingService { get; set; }
        public string? UrlSingService { get; set; }
        public bool? statusSingService { get; set; }
        public string? IdSingTransport { get; set; }
        public string? UrlSingTransport { get; set; }
        public bool? statusSingTransport { get; set; }
        public string? IdBankSingService { get; set; }
        public string? UrlBankSingService { get; set; }
        public bool? statusBankSingService { get; set; }
        public bool? AceptTerms { get; set; }
        public int? requestBeforeStatus { get; set; }
        public string? dataEnrollment { get; set; }
        public string? UrlFileJustification { get; set; }
        public byte[]? FileJustification { get; set; }
        public string? UrlReportComplete { get; set; }
        public virtual StudentPortalDTO? StudentPortal { get; set; }
        public virtual FinanceInformation? FinanceInformation { get; set; }
        public virtual ContactPortalDTO? FirstContact { get; set; }
        public virtual ContactPortalDTO? SecondContact { get; set; }
        public virtual List<AuthorizePeopleDTO>? AuthorizePeople { get; set; }
        public virtual PortalSchoolYear? PortalSchoolYear { get; set; }
        public virtual MedicalRecord? MedicalRecord { get; set; }
    }
}
