using LibraryManagement.Business.Models.Domain;

namespace LibraryManagement.Business.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book?> UpdateAsync(int id, Book book);
    }
}