using Abp.AspNetCore.SignalR.Hubs;
using Abp.RealTime;
using Castle.Core.Logging;
using Castle.Windsor;

namespace Panorama.SignalR
{
    public class AppHub : OnlineClientHubBase
    {
        private readonly IWindsorContainer _windsorContainer;
        private bool _isCallByRelease;
    
        /// <summary>
        ///     Initializes a new instance of the <see cref="AppHub" /> class.
        /// </summary>
        public AppHub(
            IWindsorContainer windsorContainer,
            IOnlineClientManager onlineClientManager,
            IOnlineClientInfoProvider clientInfoProvider) 
            : base(onlineClientManager, clientInfoProvider)
        {
            _windsorContainer = windsorContainer;
            Logger = NullLogger.Instance;
        }
    
        public void Register()
        {
            Logger.Debug("An app client is registered: " + Context.ConnectionId);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isCallByRelease)
            {
                return;
            }

            base.Dispose(disposing);
            if (disposing)
            {
                _isCallByRelease = true;
                _windsorContainer.Release(this);
            }
        }
    }
}