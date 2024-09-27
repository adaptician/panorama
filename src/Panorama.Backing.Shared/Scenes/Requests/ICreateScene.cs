namespace Panorama.Backing.Shared.Scenes.Requests
{
    public interface ICreateScene
    {
        string Name { get; set; }
    
        string Description { get; set; }
    
        string InitialSceneData { get; set; }
    }
}