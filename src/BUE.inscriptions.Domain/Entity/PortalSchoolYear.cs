using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("AnioLectivo")]
    public class PortalSchoolYear
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }
        [Column("Nombre")]
        public string name { get; set; }
        [Column("NumeroFolio")]
        public string fileNumber { get; set; }
        [Column("Estado")]
        public bool status { get; set; }
        [Column("EsActual")]
        public bool currentStatus { get; set; }
        public virtual Application? Application { get; set; }
    }
}
