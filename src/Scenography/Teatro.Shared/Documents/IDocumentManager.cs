namespace Teatro.Shared.Documents;

public interface IDocumentManager<TEntity>
{
    Task<TEntity> GetByIdAsync(string id);
    
    Task CreateAsync(TEntity document);
}