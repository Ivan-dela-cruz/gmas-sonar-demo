

using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class StudentBUEDTO
    {
        public int? studentCodeSchoolYear { get; set; }
        public int? studentCode { get; set; }
        //[Required(ErrorMessage = "El Tipo de Identificación es Obligatorio")]
        public int? typeIdentification { get; set; }
        //[Required(ErrorMessage = "El País de Nacimiento es Obligatorio")]
        public int? birthCountry { get; set; }
        //[Required(ErrorMessage = "La Nacionalidad es Obligatorio")]
        public int? primaryNationality { get; set; }
        public int? secondaryNationality { get; set; }
        public int? thirtyNationality { get; set; }
        public int? CodigoPaisResidencia { get; set; }
        public int? CodigoProvinciaResidencia { get; set; }
        public int? CodigoCantonResidencia { get; set; }
        public int? CodigoParroquiaResidencia { get; set; }
        public int? gender { get; set; }
        public int? CodigoTipoSangre { get; set; }
        public int? currentSchoolYear { get; set; }
        public int? levelCode { get; set; }
        public int? courseGradeCode { get; set; }
        public int? parallelCode { get; set; }
        public int? specialtyCode { get; set; }
        public int? civilStatus { get; set; }
        public int? language { get; set; }
        //[Required(ErrorMessage = "La Identificación es Obligatorio")]
        public string? documentNumber { get; set; }
        public string? names { get; set; }
        public string? firstName { get; set; }
        public string? completeName { get; set; }
        public DateTime? birthDate { get; set; }
        public string? fatherLastName { get; set; }
        public string? usualName { get; set; }
        public string? primaryResidence { get; set; }
        public DateTime? dateAdmission { get; set; }
        public string? previusCollege { get; set; }
        public string? birthCity { get; set; }
        public bool? status { get; set; }
        public bool? statusSchoolYear { get; set; }
        public string? familyCode { get; set; }
        public bool? isRepeatClass { get; set; }
        public bool? isEnrollmentProcess { get; set; }
        public bool? isFullDocumentation { get; set; }
        public DateTime? enrollmentDate { get; set; }
        public bool? AutorizaSalidaMedioDia { get; set; }
        public bool? AutorizaSalidaTarde { get; set; }
        public bool? Retirado { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public string? DestinoRetiro { get; set; }
        public bool? FranciaRetiro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
        public bool? CalcularInteres { get; set; }
        public bool? SeccionInternacional { get; set; }
        public byte[]? photo { get; set; }
        public string? secondName { get; set; }
        public string? Sector { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public virtual LevelDTO? level { get; set; }
        public virtual CourseGradeDTO? courseGrade { get; set; }
        public virtual CourseGradeDTO? nextGrade { get; set; }
    }
}
