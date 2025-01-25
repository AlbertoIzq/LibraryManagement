using LibraryManagement.Business.Models.Domain;

namespace LibraryManagement.Business.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book?> UpdateAsync(int id, Book book);
        Task<IEnumerable<Book>> GetAllAsync(string? filterOn = null, string? filterQuery = null);
    }
}