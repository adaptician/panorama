using System.Linq.Expressions;
using MongoDB.Driver;
using Teatro.Shared.Repositories;

namespace Teatro.Core.Repositories;

public class MongoRepository<TEntity> : IRepository<TEntity> 
    where TEntity : class
{
    private readonly IMongoCollection<TEntity> _collection;
    
    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TEntity>(collectionName);
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(string id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(string id, TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }
}