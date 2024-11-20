namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> CreateAsync(T model);
        Task<bool> DeleteAsync(int id);
        Task<T> UpdateAsync(int id, T model);
    }
}
