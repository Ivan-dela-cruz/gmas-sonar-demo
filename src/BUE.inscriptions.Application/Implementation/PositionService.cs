using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<IBaseResponse<PositionDTO>> CreateServiceAsync(PositionDTO model)
        {
            var baseResponse = new BaseResponse<PositionDTO>();
            var position = await _positionRepository.CreateAsync(model);
            baseResponse.Data = position;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _positionRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<PositionDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<PositionDTO>();
            PositionDTO position = await _positionRepository.GetByIdAsync(id);

            if (position == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = position;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<PositionDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<PositionDTO>>();
            var positions = await _positionRepository.GetAsync();

            if (positions == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<PositionDTO>.ToPagedList(positions, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PositionDTO>> UpdateServiceAsync(int id, PositionDTO model)
        {
            var baseResponse = new BaseResponse<PositionDTO>();
            PositionDTO position = await _positionRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = position;
            return baseResponse;
        }
    }
}
