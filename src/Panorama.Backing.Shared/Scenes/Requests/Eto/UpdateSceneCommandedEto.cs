using Panorama.Backing.Shared.Messages;

namespace Panorama.Backing.Shared.Scenes.Requests.Eto
{
    public class UpdateSceneCommandedEto : BrokerMessage, IUpdateScene
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}