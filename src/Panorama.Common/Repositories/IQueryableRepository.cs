namespace Panorama.Common.Repositories;

public interface IQueryableRepository<TEntity, TKey> 
    where TEntity : class
{
    IQueryable<TEntity> GetAll();
}