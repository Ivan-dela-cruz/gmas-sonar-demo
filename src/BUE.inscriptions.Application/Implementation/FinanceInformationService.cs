using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class FinanceInformationService : IFinanceInformationService
    {
        private IFinanceInformationRepository _fnInfRep;
        public FinanceInformationService(IFinanceInformationRepository fnInfRep) => _fnInfRep = fnInfRep;
        public async Task<IBaseResponse<FinanceInformationDTO>> CreateServiceAsync(FinanceInformationDTO model)
        {
            var baseResponse = new BaseResponse<FinanceInformationDTO>();
            var financeInformation = await _fnInfRep.CreateAsync(model);
            if (financeInformation is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = financeInformation;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _fnInfRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<FinanceInformationDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<FinanceInformationDTO>();
            FinanceInformationDTO financeInformation = await _fnInfRep.GetByIdAsync(id);
            if (financeInformation is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = financeInformation;
            return baseResponse;
        }
        public async Task<IBaseResponse<FinanceInformationDTO>> GetByStudentIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<FinanceInformationDTO>();
            FinanceInformationDTO financeInformation = await _fnInfRep.GetByStudentIdAsync(id);
            if (financeInformation is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = financeInformation;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<FinanceInformationDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<FinanceInformationDTO>>();
            var financeInformations = await _fnInfRep.GetAsync();
            if (financeInformations is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<FinanceInformationDTO>.ToPagedList(financeInformations, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<FinanceInformationDTO>> UpdateServiceAsync(int id, FinanceInformationDTO model)
        {

            var baseResponse = new BaseResponse<FinanceInformationDTO>();
            FinanceInformationDTO financeInformation = await _fnInfRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = financeInformation;
            return baseResponse;
        }
    }
}
