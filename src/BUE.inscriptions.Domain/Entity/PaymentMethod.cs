using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("FormaPago")]
    public class PaymentMethod
    {
        [Key]
        [Column("Codigo")]
        public int code { get; set; }

        [Column("Nombre")]
        public string name { get; set; }
    }
}
