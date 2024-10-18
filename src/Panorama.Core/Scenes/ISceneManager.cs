using Abp.Domain.Services;
using Panorama.Events;
using Panorama.Scenes.Events.SceneCreated;
using Panorama.Scenes.Events.SceneDeleted;
using Panorama.Scenes.Events.SceneReceived;
using Panorama.Scenes.Events.ScenesReceived;
using Panorama.Scenes.Events.SceneUpdated;

namespace Panorama.Scenes;

public interface ISceneManager : IEventCarrierManager, IDomainService
{
    ScenesReceivedCarrier CreateScenesReceivedCarrier();
    SceneReceivedCarrier CreateSceneReceivedCarrier();
    SceneCreatedCarrier CreateSceneCreatedCarrier();
    SceneUpdatedCarrier CreateSceneUpdatedCarrier();
    SceneDeletedCarrier CreateSceneDeletedCarrier();
}