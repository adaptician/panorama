using System.ComponentModel.DataAnnotations;

namespace Teatro.Shared.Scenes.Dtos;

public class CreateSceneDto
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SceneConstants.MaxNameLength)]
    public string Name { get; set; }
    
    [MaxLength(SceneConstants.MaxDescriptionLength)]
    public string Description { get; set; }
    
    [MaxLength(SceneConstants.MaxInitialSceneDataLength)]
    public string InitialSceneData { get; set; }
}