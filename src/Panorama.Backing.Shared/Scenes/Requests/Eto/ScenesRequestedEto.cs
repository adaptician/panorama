using Panorama.Backing.Shared.Messages;

namespace Panorama.Backing.Shared.Scenes.Requests.Eto
{
    public class ScenesRequestedEto : BrokerMessage
    {
        public string Keyword { get; set; }
        
        public int MaxResultCount { get; set; }
        
        public int SkipCount { get; set; }
    }
}