using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Reports
{
    public class TransactionDetailStudent
    {
        [Column("DOCDATE")]
        public DateTime? DocDate { get; set; }

        [Column("CodigoEstudiante")]
        public int? StudentCode { get; set; }

        [Column("SaldoTRX")]
        public decimal? TransactionBalance { get; set; }

        [Column("MontoTRX")]
        public decimal? TransactionAmount { get; set; }

        [Column("DOCNUMBR")]
        public string? DocumentNumber { get; set; }

        [Column("Grado")]
        public string? Grade { get; set; }

        [Column("Periodo")]
        public string? Period { get; set; }

        [Column("MesFacturado")]
        public string? BilledMonth { get; set; }

        [Column("PeriodoMes")]
        public int? PeriodMonth { get; set; }

        [Column("IdentificacionRE")]
        public string? REIdentification { get; set; }

        [Column("NombreRE")]
        public string? REName { get; set; }

        [Column("Estudiante")]
        public string? Student { get; set; }

        public override string ToString()
        {
            return $"DocDate: {DocDate}, StudentCode: {StudentCode}, TransactionBalance: {TransactionBalance}, TransactionAmount: {TransactionAmount}, DocumentNumber: {DocumentNumber}, Grade: {Grade}, Period: {Period}, BilledMonth: {BilledMonth}, PeriodMonth: {PeriodMonth}, REIdentification: {REIdentification}, REName: {REName}, Student: {Student}";
        }
    }
}
