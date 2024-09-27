using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Dependency;
using Abp.RealTime;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.SignalR
{
    public class SignalRAppCommunicator : IAppEventCommunicator, ITransientDependency
    {
        private readonly IHubContext<AbpCommonHub> _appHub;
        
        protected ILogger Logger;
    
        public SignalRAppCommunicator(
            IHubContext<AbpCommonHub> appHub)
        {
            _appHub = appHub;
            Logger = NullLogger.Instance;
        }
        
        public async Task SendEventToClients<TEventData>(IReadOnlyList<IOnlineClient> clients, AppEvent<TEventData> appEvent) 
            where TEventData : AppEventData, new()
        {
            foreach (var client in clients)
            {
                var signalRClient = GetSignalRClientOrNull(client);
                if (signalRClient == null)
                {
                    return;
                }

                var eventData = JsonConvert.SerializeObject(appEvent.Data);
                await signalRClient.SendAsync(appEvent.MethodName, eventData);
            }
        }
    
        public async Task SendEventToAll<TEventData>(AppEvent<TEventData> appEvent) 
            where TEventData : AppEventData, new()
        {
            var eventData = JsonConvert.SerializeObject(appEvent.Data);
            await _appHub.Clients.All.SendAsync(appEvent.MethodName, eventData);
        }
    
        private IClientProxy GetSignalRClientOrNull(IOnlineClient client)
        {
            var signalRClient = _appHub.Clients.Client(client.ConnectionId);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (signalRClient == null)
            {
                Logger.LogDebug("Can not get client user " + client.UserId + " from SignalR hub!");
                return null;
            }

            return signalRClient;
        }
    }
}