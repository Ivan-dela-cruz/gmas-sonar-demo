using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using BUE.Inscriptions.Domain.Reports;

namespace BUE.Inscriptions.Application.Implementation
{
    public class GeneralPortalService : IGeneralPortalService
    {
        private readonly IGenaralPortalRepository _generalBueRep;
        public GeneralPortalService(IGenaralPortalRepository generalBueRep) => _generalBueRep = generalBueRep;

        public async Task<IBaseResponse<PagedList<StatusDTO>>> getStatusAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<StatusDTO>>();
            var statusList = await _generalBueRep.getStatusAsync();
            if (statusList is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StatusDTO>.ToPagedList(statusList, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<SchoolYearDTO>>> getSchoolYearsAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<SchoolYearDTO>>();
            var schoolYears = await _generalBueRep.getSchoolYearsAsync();
            if (schoolYears is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<SchoolYearDTO>.ToPagedList(schoolYears, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<SchoolYearDTO>> getSchoolYearByCodeAsync(int code)
        {
            var baseResponse = new BaseResponse<SchoolYearDTO>();
            var schoolYear = await _generalBueRep.getSchoolYearByCodeAsync(code);
            if (schoolYear is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = schoolYear;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<BankDTO>>> getBankAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<BankDTO>>();
            var result = await _generalBueRep.getBankAsync();
            if (result is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<BankDTO>.ToPagedList(result, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<CreditCardDTO>>> getCreditCardsAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CreditCardDTO>>();
            var result = await _generalBueRep.getCredtCardAsync();
            if (result is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<CreditCardDTO>.ToPagedList(result, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<PaymentMethodDTO>>> getPaymentMethodAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<PaymentMethodDTO>>();
            var result = await _generalBueRep.getPaymentMethodAsync();
            if (result is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<PaymentMethodDTO>.ToPagedList(result, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<DebitTypeDTO>>> getDebitTypeAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<DebitTypeDTO>>();
            var result = await _generalBueRep.getDebitTypeAsync();
            if (result is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<DebitTypeDTO>.ToPagedList(result, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<RelationShipDTO>>> getRelationShipsAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<RelationShipDTO>>();
            var relations = await _generalBueRep.getRelationShipsAsync();
            if (relations is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<RelationShipDTO>.ToPagedList(relations, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<CivilStatusDTO>>> getCivilStatusAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CivilStatusDTO>>();
            var result = await _generalBueRep.getCivilStatusAsync();
            if (result is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<CivilStatusDTO>.ToPagedList(result, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<RecordStudentReport>>> getRecordStudentReportAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<RecordStudentReport>>();
            var result = await _generalBueRep.getRecordStudentReportAsync((int)paging.currentSchoolYear, (int)paging.studentSchoolCode, (int)paging.requestCode);
            if (result is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<RecordStudentReport>.ToPagedList(result, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

    }
}
