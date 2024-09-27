using MediatR;

namespace Panorama.Backing.Shared.Scenes.Requests.Mediations
{
    public class ScenesRequested : IRequestScenes, IRequest
    {
        public string Keyword { get; set; }
        
        public int MaxResultCount { get; set; }
        
        public int SkipCount { get; set; }
    }
}