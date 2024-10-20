using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts;
using Teatro.Contracts.Scenes.Xtos.RetrieveScenes;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.RetrieveScenes;

[MessageUrn(SceneMessageUrn.TeatroScenes_RetrieveScenes)]
public record RetrieveScenesXto : MessageXto, IRetrieveScenesXto
{
    public string? Keyword { get; init; }
        
    public int MaxResultCount { get; init; }
        
    public int SkipCount { get; init; }
}