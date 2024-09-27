using MediatR;

namespace Panorama.Backing.Shared.Scenes.Requests.Mediations
{
    public class SceneRequested : IRequestScene, IRequest
    {
        public long Id { get; set; }
    }
}