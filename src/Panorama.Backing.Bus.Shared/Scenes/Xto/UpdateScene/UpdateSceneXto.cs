using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts;
using Teatro.Contracts.Scenes.Xtos.UpdateScene;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.UpdateScene;

[MessageUrn(SceneMessageUrn.TeatroScenes_UpdateScene)]
public record UpdateSceneXto : MessageXto, IUpdateSceneXto
{
    public string SceneCorrelationId { get; init; }
    
    public string Name { get; init; }
    
    public string? Description { get; init; }
}