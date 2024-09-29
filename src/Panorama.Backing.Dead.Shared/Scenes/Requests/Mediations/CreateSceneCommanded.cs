using MediatR;

namespace Panorama.Backing.Dead.Shared.Scenes.Requests.Mediations
{
    public class CreateSceneCommanded : ICreateScene, IRequest
    {
        public string Name { get; set; }
    
        public string Description { get; set; }
    
        public string InitialSceneData { get; set; }
    }
}