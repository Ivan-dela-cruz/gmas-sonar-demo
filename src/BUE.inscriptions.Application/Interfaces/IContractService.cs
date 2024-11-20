using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.EviCertia;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IContractService
    {
        Task<IBaseResponse<string>> SendServiceContractServiceAsync(SingFileDTO modelDto);
        Task<IBaseResponse<string>> ContractGenerateServiceAsync(TemplateReport templateReport);
        Task<IBaseResponse<string>> CancelContractAsync(EvicertiaCancelDTO payload);
    }
}
