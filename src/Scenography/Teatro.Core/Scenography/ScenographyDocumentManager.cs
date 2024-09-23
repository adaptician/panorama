using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Teatro.Core.Repositories;
using Teatro.Shared.Documents;
using Teatro.Shared.Options;

namespace Teatro.Core.Scenography;

public class ScenographyDocumentManager : IDocumentManager<ScenographyDocument>
{
    private const string CollectionName = DocumentCollectionNames.Scenographies;
    
    private readonly IMongoClient _mongoClient;
    private readonly MongoDbSettings _options;
    private string _databaseName => _options.DatabaseName;

    private readonly IMongoDatabase _database;
    private readonly MongoRepository<ScenographyDocument> _documentRepository;
    
    public ScenographyDocumentManager(IMongoClient mongoClient, IOptions<MongoDbSettings> options)
    {
        _mongoClient = mongoClient;
        _options = options.Value;
        
        _database = _mongoClient.GetDatabase(_databaseName);
        _documentRepository = new MongoRepository<ScenographyDocument>(_database, CollectionName);
    }
    
    public async Task<ScenographyDocument> GetByIdAsync(string id)
    {
        return await _documentRepository.GetByIdAsync(id);
    }

    public async Task CreateAsync(ScenographyDocument document)
    {
        await _documentRepository.AddAsync(document);
    }
}