using Panorama.Backing.Shared.Messages;

namespace Panorama.Backing.Shared.Scenes.Requests.Eto
{
    public class DeleteSceneCommandedEto : IDeleteScene
    {
        public long Id { get; set; }
    }
}