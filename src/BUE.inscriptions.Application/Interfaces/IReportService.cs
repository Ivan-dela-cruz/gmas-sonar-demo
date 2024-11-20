using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.EviCertia;
using BUE.Inscriptions.Domain.Entity.DTO.storeProcedures;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IReportService
    {

        Task<IBaseResponse<ReportParameterDTO>> GetReportPDFServiceAsync(ReportParameterDTO parameters);
        Task<IBaseResponse<string>> SendServiceContractServiceAsync(SingFileDTO modelDto);
        Task<IBaseResponse<ReportParameterDTO>> GenerateContractServiceAsync(ReportParameterDTO parameters);
        Task<IBaseResponse<ReportParameterDTO>> GenerateTaskContractServiceAsync(ReportParameterDTO parameters);
        Task<IBaseResponse<SingFileDTO>> GenerateBankContractServiceAsync(SingFileDTO parameters);
        Task<IBaseResponse<ReportParameterDTO>> GetReportPDF2ServiceAsync(ReportParameterDTO parameters);
        Task<IBaseResponse<SingFileDTO>> GenerateDataContractServiceAsync(SingFileDTO parameters);
        Task<IBaseResponse<EviQuerySingDTO>> GetDocumentFromEviCertiaAsync(string uniqueKey, bool includeDocument = false);
    }
}
