using LibraryManagement.API.Data;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models.Domain;
using Microsoft.EntityFrameworkCore;

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
            // Check if it exists
            var existingBook = await _libraryDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBook == null)
            {
                return null;
            }

            // Assign updated values
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Category = book.Category;
            existingBook.IsAvailable = book.IsAvailable;

            return existingBook;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            var books = _libraryDbContext.Books.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    books = books.Where(x => x.Title.Contains(filterQuery));
                }
                else if (filterOn.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    books = books.Where(x => x.Author.Contains(filterQuery));
                }
                else if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
                {
                    books = books.Where(x => x.Category.Contains(filterQuery));
                }
            }

            return await books.ToListAsync();
        }
    }
}