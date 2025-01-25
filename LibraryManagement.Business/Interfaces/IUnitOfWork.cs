namespace LibraryManagement.Business.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        IMemberRepository Members { get; }

        // Global methods
        Task SaveAsync();
    }
}