namespace BUE.Inscriptions.Domain.Paging
{
    public class PagingQueryParameters
    {
        private const int maxPageSize = 5000;
        private int _pageSize = 5000;

        public string? search { get; set; }
        public int? courseCode { get; set; }
        public int? code { get; set; }
        public int? levelCode { get; set; }
        public int? parallelCode { get; set; }
        public int? currentSchoolYear { get; set; }
        public int? studentSchoolCode { get; set; }
        public int? requestCode { get; set; }
        public int? RequestStatus { get; set; }
        public int? AcademicYearId { get; set; }
        public int academicYear { get; set; }
        public int? userId { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => this._pageSize;
            set => this._pageSize = value > 5000 ? 5000 : value;
        }
    }
}
