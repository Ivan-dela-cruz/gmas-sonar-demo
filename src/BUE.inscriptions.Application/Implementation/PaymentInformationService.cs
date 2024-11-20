using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class PaymentInformationService : IPaymentInformationService
    {
        private readonly IPaymentInformationRepository _paymentInformationRepository;

        public PaymentInformationService(IPaymentInformationRepository paymentInformationRepository)
        {
            _paymentInformationRepository = paymentInformationRepository;
        }

        public async Task<IBaseResponse<PaymentInformationDTO>> CreateServiceAsync(PaymentInformationDTO model)
        {
            var baseResponse = new BaseResponse<PaymentInformationDTO>();
            var paymentInformation = await _paymentInformationRepository.CreateAsync(model);
            if (paymentInformation == null)
            {
                baseResponse.Message = _paymentInformationRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _paymentInformationRepository.StatusCode;
            }
            baseResponse.Data = paymentInformation;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _paymentInformationRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<PaymentInformationDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<PaymentInformationDTO>();
            PaymentInformationDTO paymentInformation = await _paymentInformationRepository.GetByIdAsync(id);

            if (paymentInformation == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = paymentInformation;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<PaymentInformationDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<PaymentInformationDTO>>();
            var paymentInformations = await _paymentInformationRepository.GetAsync();

            if (paymentInformations == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<PaymentInformationDTO>.ToPagedList(paymentInformations, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PaymentInformationDTO>> UpdateServiceAsync(int id, PaymentInformationDTO model)
        {
            var baseResponse = new BaseResponse<PaymentInformationDTO>() { Message = MessageUtil.Instance.Updated };
            PaymentInformationDTO paymentInformation = await _paymentInformationRepository.UpdateAsync(id, model);
            if (paymentInformation == null)
            {
                baseResponse.Message = _paymentInformationRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _paymentInformationRepository.StatusCode;
            }
            baseResponse.Data = paymentInformation;
            return baseResponse;
        }
    }
}
