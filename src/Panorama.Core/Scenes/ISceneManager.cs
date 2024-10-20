using Abp.Domain.Services;
using Panorama.Events;
using Panorama.Scenes.Events.SceneCreated;
using Panorama.Scenes.Events.SceneDeleted;
using Panorama.Scenes.Events.SceneRetrieved;
using Panorama.Scenes.Events.ScenesRetrieved;
using Panorama.Scenes.Events.SceneUpdated;

namespace Panorama.Scenes;

public interface ISceneManager : IEventCarrierManager, IDomainService
{
    ScenesRetrievedCarrier CreateScenesReceivedCarrier();
    SceneRetrievedCarrier CreateSceneReceivedCarrier();
    SceneCreatedCarrier CreateSceneCreatedCarrier();
    SceneUpdatedCarrier CreateSceneUpdatedCarrier();
    SceneDeletedCarrier CreateSceneDeletedCarrier();
}