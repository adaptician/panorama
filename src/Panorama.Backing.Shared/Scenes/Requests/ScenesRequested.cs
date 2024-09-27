using MediatR;

namespace Panorama.Backing.Shared.Scenes.Requests
{
    public class ScenesRequested : IRequest
    {
        public string Filter { get; set; }
        
        public int MaxResultCount { get; set; }
        
        public int SkipCount { get; set; }
    }
}