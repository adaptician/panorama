using Panorama.Backing.Shared.Common;

namespace Panorama.Backing.Shared.Messages
{
    public interface IBrokerMessage : ICorrelateMessage, ICorrelateUser, ICorrelate
    {
        BrokerMessageDeliveryModeEnum DeliveryMode { get; set; }
        
        string AppId { get; set; }
    }
}