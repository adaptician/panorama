using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos;

public interface ICreateSceneXto : IMessageXto
{
    string Name { get; init; }
    
    string Description { get; init; }
    
    /// <summary>
    /// A snapshot of the data to be used to load the scene upon initialization of rendering. 
    /// </summary>
    string SceneData { get; init; }
}