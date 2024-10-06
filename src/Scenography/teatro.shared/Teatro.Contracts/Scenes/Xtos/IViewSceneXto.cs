using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos;

public interface IViewSceneXto : IMessageXto, ICorrelateScene
{
    string Name { get; }
    
    string Description { get; }
    
    /// <summary>
    /// A snapshot of the data that is used to load the scene upon initialization of rendering. 
    /// </summary>
    string SceneData { get; }
}