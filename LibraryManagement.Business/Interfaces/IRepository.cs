using LibraryManagement.Business.Models;

namespace LibraryManagement.Business.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> DeleteAsync(int id);
    }
}