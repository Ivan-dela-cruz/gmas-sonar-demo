using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class RoleDTO
    {
        public int code { get; set; }
        [Required(ErrorMessage = "El Nombre es Obligatorio")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El nombre debe contener al menos 8 caracteres")]
        public string name { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "El Estado es obligatorio")]
        public bool status { get; set; }
        public IEnumerable<PermissionDTO>? permissions { get; set; }

    }
}
