using Panorama.Backing.Dead.Producers;
using Panorama.Backing.Dead.Shared.RoutingKeys;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Eto;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Mediations;
using Panorama.Common.Handlers;

namespace Panorama.Scenes.Handlers;

public class ScenesOperationHandler(ScenesProducer producer)
    : CrudHandler<ScenesOperation, ScenesOperationEto>(producer)
{
    protected override string RoutingKey => RoutingKeys.Operation;
}