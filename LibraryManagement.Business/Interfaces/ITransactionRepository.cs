using LibraryManagement.Business.Models.Domain;

namespace LibraryManagement.Business.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction?> BorrowBookAsync(int memberId, int bookId);
        Task<Transaction?> ReturnBookAsync(int memberId, int bookId);
        Task<IEnumerable<Book>> GetOverdueBooksAsync();
    }
}