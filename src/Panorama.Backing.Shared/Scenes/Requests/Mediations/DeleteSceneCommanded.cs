using MediatR;

namespace Panorama.Backing.Shared.Scenes.Requests.Mediations
{
    public class DeleteSceneCommanded : IDeleteScene, IRequest
    {
        public long Id { get; set; }
    }
}