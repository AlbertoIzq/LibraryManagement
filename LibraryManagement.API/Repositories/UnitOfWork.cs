using LibraryManagement.API.Data;
using LibraryManagement.Business.Interfaces;

namespace LibraryManagement.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryDbContext _libraryDbContext;

        public IBookRepository Books { get; private set; }
        public IMemberRepository Members { get; private set; }
        public ITransactionRepository Transactions { get; private set; }

        public UnitOfWork(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            Books = new BookRepository(_libraryDbContext);
            Members = new MemberRepository(_libraryDbContext);
            Transactions = new TransactionRepository(_libraryDbContext);
        }

        public async Task SaveAsync()
        {
            await _libraryDbContext.SaveChangesAsync();
        }
    }
}