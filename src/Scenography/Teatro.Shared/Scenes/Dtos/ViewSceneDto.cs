using Panorama.Backing.Shared.Scenes.Requests;
using Teatro.Shared.Bases.Dtos;

namespace Teatro.Shared.Scenes.Dtos;

public class ViewSceneDto : EntityDto<long>, IViewScene
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public long ScenographyId { get; set; }
    
    public string SceneData { get; set; }
}