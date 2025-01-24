using LibraryManagement.API.Data;
using LibraryManagement.Business.Interfaces;

namespace LibraryManagement.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryDbContext _libraryDbContext;

        public IBookRepository Books { get; private set; }

        public UnitOfWork(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            Books = new BookRepository(_libraryDbContext);
        }

        public async Task SaveAsync()
        {
            await _libraryDbContext.SaveChangesAsync();
        }
    }
}