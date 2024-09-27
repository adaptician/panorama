using Panorama.Backing.Producers;
using Panorama.Backing.Shared.RoutingKeys;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Backing.Shared.Scenes.Requests.Mediations;
using Panorama.Common.Handlers;

namespace Panorama.Scenes.Handlers;

public class SceneRequestedHandler : CrudHandler<SceneRequested, SceneRequestedEto>
{
    protected override string RoutingKey => RoutingKeys.Get;
    
    public SceneRequestedHandler(ScenesProducer producer) : base(producer)
    {
    }
}