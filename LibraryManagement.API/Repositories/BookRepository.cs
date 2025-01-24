using LibraryManagement.API.Data;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models.Domain;

namespace LibraryManagement.API.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public BookRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<Book?> UpdateAsync(int id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}