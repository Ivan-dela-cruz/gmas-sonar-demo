
using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class ContactDTO
    {
        public int? code { get; set; }
        public int? fatherCode { get; set; }
        [Required(ErrorMessage = "La Identificación es Obligatorio")]
        public string? documentNumber { get; set; }
        [Required(ErrorMessage = "El Tipo de Identificación es Obligatorio")]
        public int? typeIdentification { get; set; }
        [Required(ErrorMessage = "Los Nombres son Obligatorios")]
        public string? names { get; set; }
        [Required(ErrorMessage = "Los Apellidos son Obligatorios")]
        public string? firstName { get; set; }
        public string? completeName { get; set; }
        public string? address { get; set; }
        public string? landline { get; set; }
        public string? cellPhone { get; set; }
        public string? officePhone { get; set; }
        public string? externalPhone { get; set; }
        [Required(ErrorMessage = "El email es obligatorio")]
        public string? email { get; set; }
        [Required(ErrorMessage = "La Fecha de Nacimiento es Obligatorio")]
        public DateTime? birthDate { get; set; }
        [Required(ErrorMessage = "El tipo de sangre es obligatorio")]
        public int? typeBlood { get; set; }
        public int? gender { get; set; }
        public int? birthCountry { get; set; }
        public string? birthCity { get; set; }
        public string? workAddress { get; set; }
        public string? workPlace { get; set; }
        public bool? status { get; set; }
        public string? prefix { get; set; }
        public int? countryResidence { get; set; }
        public int? cityResidence { get; set; }
        public int? cantonCode { get; set; }
        public int? parishCode { get; set; }
        public byte[]? photo { get; set; }
        public int? primaryNationality { get; set; }
        public int? secondaryNationality { get; set; }
        public int? thirtyNationality { get; set; }
        public string? familyCode { get; set; }
        public string? password { get; set; }
        public int? language { get; set; }
        public int? professionCode { get; set; }
        public bool? isFirstLogin { get; set; }
        public string? languagePortal { get; set; }
        public bool? resetActive { get; set; }
        public string? prefixCellPhone { get; set; }
        public string? prefixLanline { get; set; }
        public string? prefixWorkPhone { get; set; }
        public List<StudentRepresentativeDTO> studentRepresentatives { get; set; }
    }
}
