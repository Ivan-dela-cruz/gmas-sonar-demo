namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IFireBaseRepository
    {
        Task<string> setValue(string node, string value);
    }
}
