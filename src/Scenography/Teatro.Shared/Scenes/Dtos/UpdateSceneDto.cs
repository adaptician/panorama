using System.ComponentModel.DataAnnotations;

namespace Teatro.Shared.Scenes.Dtos;

public class UpdateSceneDto
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SceneConstants.MaxNameLength)]
    public string Name { get; set; }
    
    [MaxLength(SceneConstants.MaxDescriptionLength)]
    public string Description { get; set; }
}