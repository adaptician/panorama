using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Teatro.Core.Bases;
using Teatro.Shared.Scenes;

namespace Teatro.Core.Scenes;

[Table(nameof(Scene), Schema = SchemaNames.Thespian)]
public class Scene : FullyAuditedEntity<long>
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SceneConstants.MaxNameLength)]
    public string Name { get; set; }
    
    
    [MaxLength(SceneConstants.MaxDescriptionLength)]
    public string Description { get; set; }
    
    
    public virtual ICollection<SceneTag> Tags { get; set; }
    
    
    [ForeignKey(nameof(Scenography))]
    public long ScenographyId { get; set; }
    public virtual Scenography.Scenography Scenography { get; set; }
}