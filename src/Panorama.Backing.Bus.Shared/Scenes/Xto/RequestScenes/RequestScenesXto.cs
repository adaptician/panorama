using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts;
using Teatro.Contracts.Scenes.Xtos.RequestScenes;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.RequestScenes;

[MessageUrn(SceneMessageUrn.TeatroScenes_RequestScenes)]
public record RequestScenesXto : MessageXto, IRequestScenesXto
{
    public string Keyword { get; init; }
        
    public int MaxResultCount { get; init; }
        
    public int SkipCount { get; init; }
}