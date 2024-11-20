using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
    public class PaymentInformationDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int? PaymentResponsibility { get; set; }
        public int IdentificationTypeId { get; set; }
        public string Identification { get; set; }
        public string PayerName { get; set; }
        public string Email { get; set; }
        public int PaymentMethod { get; set; }
        public int? BankId { get; set; }
        public int? AccountType { get; set; }
        public string? AccountNumber { get; set; }
        public int? CreditCardType { get; set; }
        public string? CreditCardNumber { get; set; }
    }
}
