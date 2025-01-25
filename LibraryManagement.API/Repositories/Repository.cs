using LibraryManagement.API.Data;
using LibraryManagement.Business.Interfaces;
using LibraryManagement.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly LibraryDbContext _libraryDbContext;
        private DbSet<T> _dbSet;

        public Repository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _dbSet = _libraryDbContext.Set<T>();

        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}