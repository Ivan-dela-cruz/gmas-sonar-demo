using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Reports;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IGenaralPortalRepository
    {
        Task<IEnumerable<StatusDTO>> getStatusAsync();
        Task<IEnumerable<SchoolYearDTO>> getSchoolYearsAsync();
        Task<IEnumerable<BankDTO>> getBankAsync();
        Task<IEnumerable<PaymentMethodDTO>> getPaymentMethodAsync();
        Task<IEnumerable<DebitTypeDTO>> getDebitTypeAsync();
        Task<IEnumerable<RelationShipDTO>> getRelationShipsAsync();
        Task<IEnumerable<CreditCardDTO>> getCredtCardAsync();
        Task<IEnumerable<CivilStatusDTO>> getCivilStatusAsync();
        Task<SchoolYearDTO> getSchoolYearByCodeAsync(int code);
        Task<IEnumerable<RecordStudentReport>> getRecordStudentReportAsync(int code, int studentCode, int requestCode);
    }
}
