using Panorama.Backing.Producers;
using Panorama.Backing.Shared.RoutingKeys;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Backing.Shared.Scenes.Requests.Mediations;
using Panorama.Common.Handlers;

namespace Panorama.Scenes.Handlers;

public class CreateSceneCommandedHandler : CrudHandler<CreateSceneCommanded, CreateSceneCommandedEto>
{
    protected override string RoutingKey => RoutingKeys.Create;
    
    public CreateSceneCommandedHandler(ScenesProducer producer) : base(producer)
    {
    }
}