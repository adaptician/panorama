using System.Linq.Expressions;

namespace Panorama.Common.Repositories;

public interface IRepository<TEntity, TKey> 
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TKey id, TEntity entity);
    Task DeleteAsync(TKey id);
}