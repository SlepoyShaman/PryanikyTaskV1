using Microsoft.EntityFrameworkCore;
using PryanikyTaskV1.Models;

namespace PryanikyTaskV1.Data
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
            => _context.Set<TEntity>().Select(p => p);

        public async Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class, IWithId
            => await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

        public async Task RemoveByIdAsync<TEntity>(int id) where TEntity : class, IWithId
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(p => p.Id == id);

            if (entity == null) throw new Exception($"{typeof(TEntity)} with id = {id} does not exist");

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddItemAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
