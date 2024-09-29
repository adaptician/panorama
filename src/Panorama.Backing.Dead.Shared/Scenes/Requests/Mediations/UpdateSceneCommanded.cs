using MediatR;

namespace Panorama.Backing.Dead.Shared.Scenes.Requests.Mediations
{
    public class UpdateSceneCommanded : IUpdateScene, IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}