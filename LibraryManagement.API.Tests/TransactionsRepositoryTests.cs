using LibraryManagement.API.Data;
using LibraryManagement.API.Repositories;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryManagement.API.Tests
{
    public class TransactionsRepositoryTests : IAsyncLifetime
    {
        private SqliteConnection _sqliteConnection;
        private LibraryDbContext _libraryDbContext;
        private ITransactionRepository _transactionRepository;

        // Arrange
        public async Task InitializeAsync()
        {
            // Create an in-memory SQLite connection for the test
            _sqliteConnection = new SqliteConnection("DataSource=:memory:");
            _sqliteConnection.Open();

            // Configure the DbContext to use the in-memory SQLite database
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlite(_sqliteConnection)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;

            _libraryDbContext = new LibraryDbContext(options);

            // Migrate the database schema, required to create tables
            await _libraryDbContext.Database.MigrateAsync();

            // Clear any pre-existing data in the database to avoid conflicts
            _libraryDbContext.Books.RemoveRange(_libraryDbContext.Books);
            _libraryDbContext.Members.RemoveRange(_libraryDbContext.Members);
            _libraryDbContext.Transactions.RemoveRange(_libraryDbContext.Transactions);
            await _libraryDbContext.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            // Dispose of the SQLite connection after the test
            await _sqliteConnection.DisposeAsync();
        }

        [Fact]
        public async Task BorrowBookAsync_ThrowsInvalidOperationException_WhenBookNotAvailable()
        {
            // Arrange
            var unavailableBook = new Book()
            {
                Id = 1,
                Title = "Philosopher's Stone",
                Author = "J. K. Rowling",
                Category = "Fantasy",
                IsAvailable = false
            };
            await _libraryDbContext.Books.AddAsync(unavailableBook);
            var member = new Member()
            {
                Id = 1,
                Name = "Member1"
            };
            await _libraryDbContext.Members.AddAsync(member);
            await _libraryDbContext.SaveChangesAsync();

            _transactionRepository = new TransactionRepository(_libraryDbContext);

            const int memberId = 1;
            const int bookId = 1;
            
            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _transactionRepository.BorrowBookAsync(memberId, bookId));
            Assert.Equal("Book not available", exception.Message);
        }
    }
}