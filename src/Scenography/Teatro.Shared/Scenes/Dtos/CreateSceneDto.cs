using System.ComponentModel.DataAnnotations;

namespace Teatro.Shared.Scenes.Dtos;

public class CreateSceneDto
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SceneConstants.MaxNameLength)]
    public string Name { get; set; }
    
    [MaxLength(SceneConstants.MaxDescriptionLength)]
    public string Description { get; set; }
    
    // TODO: this has to be an init payload, a starter.
    // This needs a max set on it.
    public string InitialSceneData { get; set; }
}