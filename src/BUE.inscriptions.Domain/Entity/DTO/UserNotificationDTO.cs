using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class UserNotificationDTO
    {
        public int code { get; set; }
        public int userCode { get; set; }
        public int? step { get; set; }
        public string? notification { get; set; }
        public string? notes { get; set; }
        public string? additionalInformation { get; set; }
        public bool send { get; set; }
        public bool read { get; set; }
        public bool done { get; set; }
        public int? studentCodeSchoolYear { get; set; }
        public bool status { get; set; }
        public string? urlImage { get; set; }
        public int user { get; set; }

    }
}
