using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _appRep;
        public ApplicationService(IApplicationRepository appRep) => _appRep = appRep;

        public async Task<IBaseResponse<ApplicationDTO>> getApplication()
        {
            var baseResponse = new BaseResponse<ApplicationDTO>();
            var appInfo = await _appRep.getApplicationAsync();
            if (appInfo is null)
            {
                baseResponse.Message = MessageUtil.Instance.Empty;
                baseResponse.status = false;
            }
            baseResponse.Data = appInfo;
            return baseResponse;
        }
        public async Task<IBaseResponse<ApplicationDTO>> UpdateServiceAsync(int id, ApplicationDTO model)
        {

            var baseResponse = new BaseResponse<ApplicationDTO>();
            ApplicationDTO user = await _appRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = user;
            return baseResponse;
        }
    }
}
