using Panorama.Backing.Producers;
using Panorama.Backing.Shared.RoutingKeys;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Backing.Shared.Scenes.Requests.Mediations;
using Panorama.Common.Handlers;

namespace Panorama.Scenes.Handlers;

public class UpdateSceneCommandedHandler(ScenesProducer producer)
    : CrudHandler<UpdateSceneCommanded, UpdateSceneCommandedEto>(producer)
{
    protected override string RoutingKey => RoutingKeys.Update;
}