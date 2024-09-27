using Panorama.Backing.Shared.Common;

namespace Panorama.Backing.Shared.Scenes.Requests
{
    public interface IUpdateScene : IIdentify<long>
    {
        string Name { get; set; }
    
        string Description { get; set; }
    }
}