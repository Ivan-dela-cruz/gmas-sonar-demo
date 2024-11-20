using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{

    [Table("AplicacionConfiguraciones")]
    public class Application
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("CodigoAnioLectivo")]
        public int? currentSchoolYear { get; set; }
        [Column("Nombre")]
        public string? name { get; set; }
        [Column("Direccion")]
        public string? address { get; set; }
        [Column("FechaInicio")]
        public DateTime? startDate { get; set; }
        [Column("FechaFin")]
        public DateTime? endDate { get; set; }
        [Column("Estado")]
        public bool? Status { get; set; }
        [Column("ProcesoMatricula")]
        public bool? isEnrollment { get; set; }
        [Column("CodigoAnioLectivoAnterior")]
        public int? afterSchoolYear { get; set; }
        [Column("photo")]
        public byte[]? photo { get; set; }
        [Column("urlImage")]
        public string? urlImage { get; set; }
        [Column("ruc")]
        public string? ruc { get; set; }
        [Column("email")]
        public string? email { get; set; }
        [Column("telefono")]
        public string? phone { get; set; }
        [Column("celular")]
        public string? cellPhone { get; set; }
        [Column("descripcion")]
        public string? description { get; set; }
        [Column("informacionAdicional")]
        public string? additionalInformation { get; set; }
        public virtual PortalSchoolYear? SchoolYear { get; set; }

    }

}


