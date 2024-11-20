using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.EviCertia;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IExcelReportService
    {
        Task<IBaseResponse<byte[]>> TransportExcelGenerateServiceAsync(TemplateReport templateReport);
        Task<IBaseResponse<byte[]>> AutorizationsExcelGenerateServiceAsync(TemplateReport templateReport);
    }
}
