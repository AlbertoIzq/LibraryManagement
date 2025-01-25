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

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> DeleteAsync(int id)
        {
            // Check if it exists
            var existingEntity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (existingEntity == null)
            {
                return null;
            }

            // Delete entity
            _dbSet.Remove(existingEntity);

            return existingEntity;
        }
    }
}