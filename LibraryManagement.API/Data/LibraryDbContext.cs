using LibraryManagement.Business.Models;
using LibraryManagement.Business.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LibraryManagement.API.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed to the database
            modelBuilder.Entity<Book>().HasData(BooksIniData());
        }

        // Data to seed
        private List<Book> BooksIniData()
        {
            var _books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Philosopher's Stone",
                    Author = "J. K. Rowling",
                    Category = "Fantasy"
                },
                new Book()
                {
                    Id = 2,
                    Title = "Chamber of Secrets",
                    Author = "J. K. Rowling",
                    Category = "Fantasy"
                },
                new Book()
                {
                    Id = 3,
                    Title = "Prisoner of Azkaban",
                    Author = "J. K. Rowling",
                    Category = "Fantasy"
                },
                new Book()
                {
                    Id = 4,
                    Title = "Goblet of Fire",
                    Author = "J. K. Rowling",
                    Category = "Fantasy"
                },
                new Book()
                {
                    Id = 5,
                    Title = "The Fellowship of the Ring",
                    Author = "J. R. R. Tolkien",
                    Category = "Fantasy"
                },
                new Book()
                {
                    Id = 6,
                    Title = "The Two Towers",
                    Author = "J. R. R. Tolkien",
                    Category = "Fantasy"
                },
                new Book()
                {
                    Id = 7,
                    Title = "The Return of the King",
                    Author = "J. R. R. Tolkien",
                    Category = "Fantasy"
                },
                new Book()
                {
                    Id = 8,
                    Title = "1984",
                    Author = "George Orwell",
                    Category = "Dystopian"
                }
            };

            return _books;
        }
    }
}