namespace BUE.Inscriptions.Domain.Entity
{
    public interface ISoftDelete
    {
        DateTime? DeletedAt { get; set; }
    }
}
