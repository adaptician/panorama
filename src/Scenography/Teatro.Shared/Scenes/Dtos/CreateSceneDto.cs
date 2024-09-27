using System.ComponentModel.DataAnnotations;
using Panorama.Backing.Shared.Scenes.Requests;

namespace Teatro.Shared.Scenes.Dtos;

// TODO: remove project reference to Teatro shared from Panorama.
public class CreateSceneDto : ICreateScene
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SceneConstants.MaxNameLength)]
    public string Name { get; set; }
    
    [MaxLength(SceneConstants.MaxDescriptionLength)]
    public string Description { get; set; }
    
    [MaxLength(SceneConstants.MaxInitialSceneDataLength)]
    public string InitialSceneData { get; set; }
}