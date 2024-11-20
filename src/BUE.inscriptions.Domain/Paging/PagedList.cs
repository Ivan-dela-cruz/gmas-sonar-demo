namespace BUE.Inscriptions.Domain.Paging
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }

        public int TotalPages { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPrevious => this.CurrentPage > 1;

        public bool HasNext => this.CurrentPage < this.TotalPages;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            this.TotalCount = count;
            this.PageSize = pageSize;
            this.CurrentPage = pageNumber;
            this.TotalPages = (int)Math.Ceiling((double)count / (double)pageSize);
            this.AddRange((IEnumerable<T>)items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count<T>();
            return new PagedList<T>(source.Skip<T>((pageNumber - 1) * pageSize).Take<T>(pageSize).ToList<T>(), count, pageNumber, pageSize);
        }
    }
}
