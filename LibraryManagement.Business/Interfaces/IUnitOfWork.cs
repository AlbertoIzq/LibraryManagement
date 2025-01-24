namespace LibraryManagement.Business.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }

        // Global methods
        Task SaveAsync();
    }
}