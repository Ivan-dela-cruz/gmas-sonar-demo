using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Inscriptions.Entities
{
    [Table("PaymentInformation")]
    public class PaymentInformation
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [ForeignKey("Student")]
        [Column("StudentId")]
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
