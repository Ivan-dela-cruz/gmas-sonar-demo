
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class UserRoleDTO
    {
        public int? code { get; set; }
        public int roleCode { get; set; }
        public int userCode { get; set; }
    }
}

