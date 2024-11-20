using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IBaseService<T>
    {
        Task<IBaseResponse<PagedList<T>>> GetServiceAsync(PagingQueryParameters paging);
        Task<IBaseResponse<T>> CreateServiceAsync(T entity);
        Task<IBaseResponse<T>> UpdateServiceAsync(int id, T entity);
        Task<IBaseResponse<bool>> DeleteServiceAsync(int id);
    }
}
