using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class StatusDTO
    {
        public int code { get; set; }
        public string name { get; set; }
        public string lang { get; set; }
        public string? UrlPage { get; set; }
        public int value { get; set; }
        public string? process { get; set; }
        public bool? status { get; set; }
        public int? Order { get; set; }

    }
}
