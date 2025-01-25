using LibraryManagement.API.Data;
using LibraryManagement.Business;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Repositories
{
    public class TransactionRepository :ITransactionRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public TransactionRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<Transaction?> BorrowBookAsync(int memberId, int bookId)
        {
            var book = await _libraryDbContext.Books.FindAsync(bookId);
            if (book is null)
            {
                throw new InvalidOperationException("Book not found");
            }
            if (!book.IsAvailable)
            {
                throw new InvalidOperationException("Book not available");
            }

            var member = await _libraryDbContext.Members.FindAsync(memberId);
            if (member is null)
            {
                throw new InvalidOperationException("Member not found");
            }

            // Prevent borrowing if the maximum limit or overdue books exist
            var memberTransactions = await _libraryDbContext.Transactions.Where(x => x.MemberId == memberId).ToListAsync();
            if (memberTransactions.Where(x => x.ReturnedAt is null).Count() >= Constants.MAX_NUMBER_BORROWED_BOOKS)
            {
                throw new InvalidOperationException("Member cannot borrow more books because the limit has been reached");
            }
            if (memberTransactions.Any(x => x.BorrowedAt.AddDays(Constants.MAX_NUMBER_BORROWING_DAYS) < DateTime.UtcNow))
            {
                throw new InvalidOperationException("Member cannot borrow more books because there are overdue books");
            }

            var transaction = new Transaction
            {
                BookId = bookId,
                MemberId = memberId,
                BorrowedAt = DateTime.UtcNow
            };

            book.IsAvailable = false;
            _libraryDbContext.Transactions.Add(transaction);

            return transaction;
        }

        public async Task<Transaction?> ReturnBookAsync(int memberId, int bookId)
        {
            var transaction = await _libraryDbContext.Transactions.FirstOrDefaultAsync(x => x.MemberId == memberId
                && x.BookId == bookId && x.ReturnedAt == null);
            if (transaction == null)
            {
                throw new InvalidOperationException("Invalid transaction");
            }
            transaction.ReturnedAt = DateTime.UtcNow;
            var book = await _libraryDbContext.Books.FindAsync(transaction.BookId);
            book.IsAvailable = true;

            return transaction;
        }

        public Task<IEnumerable<Transaction>> GetOverdueBooks(int memberId)
        {
            throw new NotImplementedException();
        }
    }
}