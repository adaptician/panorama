using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos.CreateScene;

public interface ICreateSceneXto : IMessageXto
{
    string Name { get; }
    
    string Description { get; }
    
    /// <summary>
    /// A snapshot of the data to be used to load the scene upon initialization of rendering. 
    /// </summary>
    string SceneData { get; }
}