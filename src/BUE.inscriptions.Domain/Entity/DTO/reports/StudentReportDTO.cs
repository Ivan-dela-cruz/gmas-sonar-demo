namespace BUE.Inscriptions.Domain.Entity.DTO.reports
{
    public class StudentReportDTO
    {
        public bool? allRegisters { get; set; }
        public bool? sigleRegister { get; set; }
        public bool? boxFilters { get; set; }
        public int? student { get; set; }
        public bool? beneficiaryScholarShip { get; set; }
        public int? statusRequest { get; set; }
        public int? section { get; set; }
        public int? course { get; set; }
        public bool? transport { get; set; }
        public bool? exitAuthorization { get; set; }
    }
}
