namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class GenericDTO<T> : List<T>
    {
        public IEnumerable<T> collection { get; set; }
    }
}
