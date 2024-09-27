using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Panorama.Events.Base;

namespace Panorama.Events;

public interface IAppEventManager : IDomainService
{
    /// <summary>
    /// Broadcast event to the client devices of a specific user.
    /// </summary>
    /// <param name="appEvent"></param>
    /// <param name="receiver"></param>
    /// <typeparam name="TEventData"></typeparam>
    /// <returns></returns>
    Task SendEventAsync<TEventData>(AppEvent<TEventData> appEvent, UserIdentifier receiver)
        where TEventData : AppEventData, new();

    /// <summary>
    /// Broadcast event to all client devices.
    /// </summary>
    /// <param name="appEvent"></param>
    /// <typeparam name="TEventData"></typeparam>
    /// <returns></returns>
    Task SendEventAsync<TEventData>(AppEvent<TEventData> appEvent)
        where TEventData : AppEventData, new();
}