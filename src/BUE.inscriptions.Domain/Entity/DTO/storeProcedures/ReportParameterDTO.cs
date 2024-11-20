namespace BUE.Inscriptions.Domain.Entity.DTO.storeProcedures
{
    public class ReportParameterDTO
    {
        public int? userCode { get; set; }
        public int? currentYearSchool { get; set; }
        public int? studentCodeSchoolYear { get; set; }
        public int? contactCodeFirst { get; set; }
        public int? contactCodeSecond { get; set; }
        public int? requestCode { get; set; }
        public int? reportCode { get; set; }
        public string? reportName { get; set; }
        public string? reportType { get; set; }
        public string? reportResult { get; set; }
        public string? reportStatus { get; set; }
        public DateTime? date { get; set; }

        public bool? beneficiaryScholarShip { get; set; }
        public bool? transport { get; set; }
        public bool? exitAuthorization { get; set; }
        public int? statusRequest { get; set; }
        public int? level { get; set; }
        public int? course { get; set; }
        public int? typeRepresentative { get; set; }
        public int? professionalStatus { get; set; }
        public byte[]? File { get; set; }
        public string? Instruction { get; set; }
        public string? code { get; set; }

    }
}
