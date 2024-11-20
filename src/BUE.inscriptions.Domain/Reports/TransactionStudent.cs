using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Reports
{
    public class TransactionStudent
    {


        [Column("CodigoEstudiante")]
        public int? StudentCode { get; set; }

        [Column("SaldoTRX")]
        public decimal? TransactionBalance { get; set; }

        public override string ToString()
        {
            return $"StudentCode: {StudentCode}, TransactionBalance: {TransactionBalance}";
        }
    }
}
