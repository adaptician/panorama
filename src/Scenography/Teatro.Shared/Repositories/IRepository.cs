using System.Linq.Expressions;

namespace Teatro.Shared.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(string id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(string id, TEntity entity);
    Task DeleteAsync(string id);
}