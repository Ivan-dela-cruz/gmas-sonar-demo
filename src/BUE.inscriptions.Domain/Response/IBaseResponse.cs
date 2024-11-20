using BUE.Inscriptions.Domain.Paging;
namespace BUE.Inscriptions.Domain.Response
{
    public interface IBaseResponse<T>
    {
        T? Data { get; set; }

        Paginate? paginate { get; set; }

        string statusCode { get; set; }

        string Message { get; set; }

        bool status { get; set; }
    }
}
