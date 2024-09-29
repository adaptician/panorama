using Teatro.Shared.Bases.Dtos;

namespace Teatro.Shared.Scenes.Dtos;

// TODO: do not expose db id's
public class ViewSceneDto : EntityDto<long>
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    // TODO: do not expose db id's
    public long ScenographyId { get; set; }
    
    public string SceneData { get; set; }
}