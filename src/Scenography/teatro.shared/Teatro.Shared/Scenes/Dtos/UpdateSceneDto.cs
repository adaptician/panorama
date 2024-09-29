using System.ComponentModel.DataAnnotations;
using Teatro.Shared.Bases.Dtos;

namespace Teatro.Shared.Scenes.Dtos;

// TODO: do not expose db id's
public class UpdateSceneDto : EntityDto<long>
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SceneConstants.MaxNameLength)]
    public string Name { get; set; }
    
    [MaxLength(SceneConstants.MaxDescriptionLength)]
    public string Description { get; set; }
}