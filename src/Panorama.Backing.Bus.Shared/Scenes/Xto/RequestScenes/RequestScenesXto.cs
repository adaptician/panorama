using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts.Scenes.Xtos.RequestScenes;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.RequestScenes;

[MessageUrn("Teatro.Scenes:RequestScenesXto")]
public record RequestScenesXto : MessageXto, IRequestScenesXto
{
    public string Keyword { get; init; }
        
    public int MaxResultCount { get; init; }
        
    public int SkipCount { get; init; }
}