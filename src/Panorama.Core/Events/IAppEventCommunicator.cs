using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.RealTime;
using Panorama.Events.Base;

namespace Panorama.Events;

public interface IAppEventCommunicator
{
    Task SendEventToClients<TEventData>(IReadOnlyList<IOnlineClient> clients, AppEvent<TEventData> appEvent) 
        where TEventData : AppEventData, new();

    Task SendEventToAll<TEventData>(AppEvent<TEventData> appEvent)
        where TEventData : AppEventData, new();
}