using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.Scenes.Requests;

namespace Teatro.Shared.Scenes.Etos;

public class ViewSceneEto : BrokerMessage, IViewScene
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long ScenographyId { get; set; }
    public string SceneData { get; set; }
}