namespace LibraryManagement.Business.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        IMemberRepository Members { get; }
        ITransactionRepository Transactions { get; }

        // Global methods
        Task SaveAsync();
    }
}