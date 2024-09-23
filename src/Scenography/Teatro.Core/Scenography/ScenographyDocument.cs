using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Teatro.Core.Scenography;

/// <summary>
/// This entity resides in a Mongo database.
/// </summary>
public class ScenographyDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
    
    public string SceneData { get; set; }

    public ScenographyDocument(Guid guid, string sceneData)
    {
        Id = guid.ToString();
        SceneData = sceneData;
    }
}