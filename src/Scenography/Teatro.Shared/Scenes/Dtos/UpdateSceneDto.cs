using System.ComponentModel.DataAnnotations;
using Teatro.Shared.Bases.Dtos;

namespace Teatro.Shared.Scenes.Dtos;

public class UpdateSceneDto : EntityDto<long>
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SceneConstants.MaxNameLength)]
    public string Name { get; set; }
    
    [MaxLength(SceneConstants.MaxDescriptionLength)]
    public string Description { get; set; }
}