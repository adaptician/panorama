using Panorama.Backing.Shared.Common;

namespace Panorama.Backing.Shared.Scenes.Requests
{
    // TODO: the use of a sequential id on this is probably bad.
    public interface IViewScene : IIdentify<long>
    {
        string Name { get; set; }
    
        string Description { get; set; }
    
        long ScenographyId { get; set; }
    
        string SceneData { get; set; }
    }
}