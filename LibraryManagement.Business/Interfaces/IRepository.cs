namespace LibraryManagement.Business.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> DeleteAsync(int id);
    }
}