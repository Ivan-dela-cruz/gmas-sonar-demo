using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class WelcomeRequestDTO
    {
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public int? RequestStatus { get; set; }
        public int? StudentCodeSchoolYear { get; set; }
        public int StudentCode { get; set; }
        public int? YearSchool { get; set; }
        public byte[]? Image { get; set; }
        public string? UrlImage { get; set; }
        public string Name { get; set; }
        //public T AdditionalInformation { get; set; }
        
    }
}
