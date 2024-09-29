using Panorama.Backing.Dead.Shared.Common;

namespace Panorama.Backing.Dead.Shared.Scenes.Requests.Eto
{
    public class SceneRequestedEto : IIdentify<long>
    {
        public long Id { get; set; }
    }
}