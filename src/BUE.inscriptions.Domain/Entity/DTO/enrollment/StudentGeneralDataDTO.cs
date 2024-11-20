using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity.DTO.enrollment
{
    public class StudentGeneralDataDTO
    {
        [Required(ErrorMessage = "El genero es Obligatorio")]
        public int? gender { get; set; }
        [Required(ErrorMessage = "El primer apellido es Obligatorio")]
        public string? firstName { get; set; }
        [Required(ErrorMessage = "El segundo apellido es Obligatorio")]
        public string? sencondName { get; set; }
        [Required(ErrorMessage = "Los nombres son Obligatorios")]
        public string? names { get; set; }
        [Required(ErrorMessage = "El nombre Usual es Obligatorio")]
        public string? usualName { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es Obligatorio")]
        public DateTime? birthDate { get; set; }
        [Required(ErrorMessage = "El País de Nacimiento es Obligatorio")]
        public int? birthCountry { get; set; }
        [Required(ErrorMessage = "La ciudad de nacimiento es Obligatorio")]
        public string? birthCity { get; set; }
        [Required(ErrorMessage = "El lugar del domicilio principal del alumno Obligatorio")]
        public string? primaryResidence { get; set; }
        [Required(ErrorMessage = "La nacionalidad es Obligatorio")]
        public int? primaryNationality { get; set; }
        public int? secondaryNationality { get; set; }
        [Required(ErrorMessage = "El documento de identidad es Obligatorio")]
        public int? documentFile { get; set; }
        [Required(ErrorMessage = "El Tipo de Identificación es Obligatorio")]
        public int? typeIdentification { get; set; }
        [Required(ErrorMessage = "El número de Identificación es Obligatorio")]
        public string? documentNumber { get; set; }
    }
}
