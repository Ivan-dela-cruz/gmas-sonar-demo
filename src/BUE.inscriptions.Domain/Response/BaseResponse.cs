using BUE.Inscriptions.Domain.Paging;

namespace BUE.Inscriptions.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T? Data { get; set; }

        public Paginate? paginate { get; set; }

        public string Message { get; set; } = "";

        public bool status { get; set; } = true;

        public string statusCode { get; set; } = "";
    }
}
