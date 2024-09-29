using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Abp.RealTime;
using Panorama.Events.Base;

namespace Panorama.Events;

public class AppEventManager : DomainService, IAppEventManager
{
    private readonly IAppEventCommunicator _appCommunicator;
    private readonly IOnlineClientManager _onlineClientManager;

    public AppEventManager(IAppEventCommunicator appCommunicator, 
        IOnlineClientManager onlineClientManager)
    {
        _appCommunicator = appCommunicator;
        _onlineClientManager = onlineClientManager;
    }
    
    public async Task SendEventAsync<TEventData>(AppEvent<TEventData> appEvent, UserIdentifier receiver)
        where TEventData : AppEventData, new()
    {
        var clients = await _onlineClientManager.GetAllByUserIdAsync(receiver);
        if (clients.Any())
        {
            await _appCommunicator.SendEventToClients(clients, appEvent);
        }
    }
    
    public async Task SendEventAsync<TEventData>(AppEvent<TEventData> appEvent)
        where TEventData : AppEventData, new()
    {
        await _appCommunicator.SendEventToAll(appEvent);
    }
}