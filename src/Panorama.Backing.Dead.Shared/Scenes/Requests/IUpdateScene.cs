using Panorama.Backing.Dead.Shared.Common;

namespace Panorama.Backing.Dead.Shared.Scenes.Requests
{
    public interface IUpdateScene : IIdentify<long>
    {
        string Name { get; set; }
    
        string Description { get; set; }
    }
}