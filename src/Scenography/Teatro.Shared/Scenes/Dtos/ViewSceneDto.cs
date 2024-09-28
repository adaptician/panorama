using Panorama.Backing.Shared.Scenes.Requests;
using Teatro.Shared.Bases.Dtos;

namespace Teatro.Shared.Scenes.Dtos;

// TODO: destroy these DTO's and ETO's - they must all come from backing shared.
public class ViewSceneDto : EntityDto<long>, IViewScene
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public long ScenographyId { get; set; }
    
    public string SceneData { get; set; }
}