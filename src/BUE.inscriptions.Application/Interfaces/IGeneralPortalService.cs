using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Reports;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IGeneralPortalService
    {
        Task<IBaseResponse<PagedList<StatusDTO>>> getStatusAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<SchoolYearDTO>>> getSchoolYearsAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<BankDTO>>> getBankAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<PaymentMethodDTO>>> getPaymentMethodAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<DebitTypeDTO>>> getDebitTypeAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<RelationShipDTO>>> getRelationShipsAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<CreditCardDTO>>> getCreditCardsAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<CivilStatusDTO>>> getCivilStatusAsync(PagingQueryParameters paging);
        Task<IBaseResponse<SchoolYearDTO>> getSchoolYearByCodeAsync(int code);
        Task<IBaseResponse<PagedList<RecordStudentReport>>> getRecordStudentReportAsync(PagingQueryParameters paging);
    }
}
