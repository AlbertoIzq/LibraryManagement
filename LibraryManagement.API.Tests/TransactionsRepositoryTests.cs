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
            await _libraryDbContext.Books.AddAsync(GetBooksWhenBookNotAvailable());
            await _libraryDbContext.Members.AddAsync(GetMember());
            await _libraryDbContext.SaveChangesAsync();

            _transactionRepository = new TransactionRepository(_libraryDbContext);

            const int memberId = 1;
            const int bookId = 1;
            
            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _transactionRepository.BorrowBookAsync(memberId, bookId));
            Assert.Equal("Book not available", exception.Message);
        }

        [Fact]
        public async Task BorrowBookAsync_ThrowsInvalidOperationException_WhenBookLimitReached()
        {
            // Arrange
            await _libraryDbContext.Books.AddRangeAsync(GetBooksWhenBookLimitReached());
            await _libraryDbContext.Members.AddAsync(GetMember());
            await _libraryDbContext.Transactions.AddRangeAsync(GetTransactionsWhenBookLimitReached());
            await _libraryDbContext.SaveChangesAsync();

            _transactionRepository = new TransactionRepository(_libraryDbContext);

            const int memberId = 1;
            const int bookId = 6;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _transactionRepository.BorrowBookAsync(memberId, bookId));
            Assert.Equal("Member cannot borrow more books because the limit has been reached", exception.Message);
        }

        private Member GetMember()
        {
            return new Member()
            {
                Id = 1,
                Name = "Member1"
            };
        }

        private Book GetBooksWhenBookNotAvailable()
        {
            return new Book()
            {
                Id = 1,
                Title = "Philosopher's Stone",
                Author = "J. K. Rowling",
                Category = "Fantasy",
                IsAvailable = false
            };
        }

        private List<Book> GetBooksWhenBookLimitReached()
        {
            return new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Philosopher's Stone",
                    Author = "J. K. Rowling",
                    Category = "Fantasy",
                    IsAvailable = false
                },
                new Book()
                {
                    Id = 2,
                    Title = "Chamber of Secrets",
                    Author = "J. K. Rowling",
                    Category = "Fantasy",
                    IsAvailable = false
                },
                new Book()
                {
                    Id = 3,
                    Title = "Prisoner of Azkaban",
                    Author = "J. K. Rowling",
                    Category = "Fantasy",
                    IsAvailable = false
                },
                new Book()
                {
                    Id = 4,
                    Title = "Goblet of Fire",
                    Author = "J. K. Rowling",
                    Category = "Fantasy",
                    IsAvailable = false
                },
                new Book()
                {
                    Id = 5,
                    Title = "The Fellowship of the Ring",
                    Author = "J. R. R. Tolkien",
                    Category = "Fantasy",
                    IsAvailable = false
                },
                new Book()
                {
                    Id = 6,
                    Title = "The Two Towers",
                    Author = "J. R. R. Tolkien",
                    Category = "Fantasy"
                }
            };
        }

        private List<Transaction> GetTransactionsWhenBookLimitReached()
        {
            return new List<Transaction>()
            {
                new Transaction()
                {
                    Id = 1,
                    MemberId = 1,
                    BookId = 1,
                    BorrowedAt = DateTime.UtcNow
                },
                new Transaction()
                {
                    Id = 2,
                    MemberId = 1,
                    BookId = 2,
                    BorrowedAt = DateTime.UtcNow
                },
                new Transaction()
                {
                    Id = 3,
                    MemberId = 1,
                    BookId = 3,
                    BorrowedAt = DateTime.UtcNow
                },
                new Transaction()
                {
                    Id = 4,
                    MemberId = 1,
                    BookId = 4,
                    BorrowedAt = DateTime.UtcNow
                },
                new Transaction()
                {
                    Id = 5,
                    MemberId = 1,
                    BookId = 5,
                    BorrowedAt = DateTime.UtcNow
                }
            };
        }
    }
}