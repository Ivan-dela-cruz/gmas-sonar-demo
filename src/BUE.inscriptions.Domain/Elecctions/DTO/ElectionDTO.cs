using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BUE.Inscriptions.Domain.Inscriptions.DTO;

namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class ElectionDTO
    {
        public int Id { get; set; }
        public int AcademicYearId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public DateTime? DateElection { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string CoverImage { get; set; }
        public byte[]? CoverImageFile { get; set; }
        public string ThumbnailImage { get; set; }
        public string ElectionType { get; set; }
        public string AcademicPeriod { get; set; }
        public int? Year { get; set; }
        public string Results { get; set; }
        public bool Status { get; set; }
        public int Seats { get; set; }
        public string StatusElection { get; set; }
        public int Group { get; set; }

        // Related Model DTOs
        public AcademicYearDTO? AcademicYear { get; set; }
        public VoteSummaryDTO<VoteCountDTO>? Summary { get; set; }
    }
}
