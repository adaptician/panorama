using Panorama.Backing.Shared.Common;

namespace Panorama.Backing.Shared.Scenes.Requests.Eto
{
    public class ScenesRequestedEto : ICorrelateUser
    {
        public string Filter { get; set; }
        
        public int MaxResultCount { get; set; }
        
        public int SkipCount { get; set; }
        
        public string UserId { get; set; }
    }
}