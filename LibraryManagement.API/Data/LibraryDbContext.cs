using LibraryManagement.Business.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LibraryManagement.API.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}