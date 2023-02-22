using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PryanikyTaskV1.Models;

namespace PryanikyTaskV1.Data
{
    public interface IRepository
    {
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        public Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class, IWithId;
        public Task RemoveByIdAsync<TEntity>(int id) where TEntity : class, IWithId;
        public Task AddItemAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
