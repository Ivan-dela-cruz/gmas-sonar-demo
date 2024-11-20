namespace BUE.Inscriptions.Domain.Entity
{
    public abstract class BaseEntity : ISoftDelete
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
