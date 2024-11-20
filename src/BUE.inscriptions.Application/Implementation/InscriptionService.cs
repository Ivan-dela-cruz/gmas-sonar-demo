using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class InscriptionService : IInscriptionService
    {
        private readonly IInscriptionRepository _inscriptionRepository;

        public InscriptionService(IInscriptionRepository inscriptionRepository)
        {
            _inscriptionRepository = inscriptionRepository;
        }

        public async Task<IBaseResponse<InscriptionDTO>> CreateServiceAsync(InscriptionDTO model)
        {
            var baseResponse = new BaseResponse<InscriptionDTO>();
            var inscription = await _inscriptionRepository.CreateAsync(model);
            if (inscription == null)
            {
                baseResponse.Message = _inscriptionRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _inscriptionRepository.StatusCode;
            }
            baseResponse.Data = inscription;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _inscriptionRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<InscriptionDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<InscriptionDTO>();
            InscriptionDTO inscription = await _inscriptionRepository.GetByIdAsync(id);

            if (inscription == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = inscription;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<InscriptionDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<InscriptionDTO>>();
            var inscriptions = await _inscriptionRepository.GetAsync();

            if (inscriptions == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<InscriptionDTO>.ToPagedList(inscriptions, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }  
        public async Task<IBaseResponse<PagedList<StudentDTO>>> GetByAcademicYearAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<StudentDTO>>();
            var inscriptions = await _inscriptionRepository.GetByAcademicYearAsync(paging);

            if (inscriptions == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<StudentDTO>.ToPagedList(inscriptions, paging.PageNumber, paging.PageSize);
            return baseResponse;
        } 

        public async Task<IBaseResponse<InscriptionDTO>> UpdateServiceAsync(int id, InscriptionDTO model)
        {
            var baseResponse = new BaseResponse<InscriptionDTO>() { Message = MessageUtil.Instance.Updated };
            InscriptionDTO inscription = await _inscriptionRepository.UpdateAsync(id, model);
            if (inscription == null)
            {
                baseResponse.Message = _inscriptionRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _inscriptionRepository.StatusCode;
            }
            baseResponse.Data = inscription;
            return baseResponse;
        } 
        public async Task<IBaseResponse<bool>> UpdateStatusServiceAsync(RequestStatusChange requestStatus)
        {
            var baseResponse = new BaseResponse<bool>() { Message = MessageUtil.Instance.Updated };
            var result = await _inscriptionRepository.UpdateStatusAsync(requestStatus);
            if (result == false)
            {
                baseResponse.Message = _inscriptionRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _inscriptionRepository.StatusCode;
            }
            baseResponse.Data = result;
            return baseResponse;
        }
        public async Task<IBaseResponse<InscriptionDTO>> UpdateProcessServiceAsync(int id, InscriptionDTO model)
        {
            var baseResponse = new BaseResponse<InscriptionDTO>() { Message = MessageUtil.Instance.Updated };
            InscriptionDTO inscription = await _inscriptionRepository.UpdateProcessAsync(id, model);
            if (inscription == null)
            {
                baseResponse.Message = _inscriptionRepository.Message;
                baseResponse.status = false;
                baseResponse.statusCode = _inscriptionRepository.StatusCode;
            }
            baseResponse.Data = inscription;
            return baseResponse;
        }
    }
}
