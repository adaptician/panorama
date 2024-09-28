using Microsoft.EntityFrameworkCore;
using Panorama.Common.Repositories;

namespace Teatro.EntityFrameworkCore.Repositories.Base;

public class Repository<TEntity, TKey> : IQueryableRepository<TEntity, TKey> 
    where TEntity : class
{
    private readonly TeatroDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    
    public Repository(TeatroDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    
    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsQueryable();
    }
}