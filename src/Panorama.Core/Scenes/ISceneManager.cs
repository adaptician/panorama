using Abp.Domain.Services;
using Panorama.Scenes.Events.ScenesReceived;

namespace Panorama.Scenes;

public interface ISceneManager : IDomainService
{
    ScenesReceivedCarrier CreateScenesReceivedCarrier();
}