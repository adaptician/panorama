using Panorama.Backing.Shared.Messages;

namespace Panorama.Backing.Shared.Scenes.Requests.Eto
{
    public class CreateSceneCommandedEto : BrokerMessage, ICreateScene
    {
        public string Name { get; set; }
    
        public string Description { get; set; }
    
        public string InitialSceneData { get; set; }
    }
}