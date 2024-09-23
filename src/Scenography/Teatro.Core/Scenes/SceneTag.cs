using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Teatro.Core.Bases;
using Teatro.Shared.Scenes;

namespace Teatro.Core.Scenes;

[Table(nameof(SceneTag), Schema = SchemaNames.Thespian)]
public class SceneTag : Entity<long>
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(TagConstants.MaxNameLength)]
    public string Name { get; set; }
    
    
    [ForeignKey(nameof(Scene))]
    public long SceneId { get; set; }
    public virtual Scene Scene { get; set; }
}