using Panorama.Backing.Shared.Common;
using Panorama.Backing.Shared.Messages;

namespace Panorama.Backing.Shared.Scenes.Requests.Eto
{
    public class SceneRequestedEto : BrokerMessage, IIdentify<long>
    {
        public long Id { get; set; }
    }
}