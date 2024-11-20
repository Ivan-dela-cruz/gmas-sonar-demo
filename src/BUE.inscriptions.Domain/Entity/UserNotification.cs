using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("UserNotification")]
    public class UserNotification : BaseEntity
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("codigoUsuario")]
        public int userCode { get; set; }
        [Column("step")]
        public int? step { get; set; }
        [Column("notificacion")]
        public string? notification { get; set; }
        [Column("notas")]
        public string? notes { get; set; }
        [Column("InformacionAdicional")]
        public string? additionalInformation { get; set; }
        [Column("enviado")]
        public bool send { get; set; }
        [Column("leido")]
        public bool read { get; set; }
        [Column("realizado")]
        public bool done { get; set; }
        [Column("CodigoEstudianteAnioLectivo")]
        public int? studentCodeSchoolYear { get; set; }
        [Column("Estado")]
        public bool status { get; set; }
        [Column("urlImage")]
        public string? urlImage { get; set; }
        [Column("user")]
        public int user { get; set; }
    }
}
