using System;

namespace Panorama.Backing.Shared.Messages
{
    public class BrokerMessage : IBrokerMessage
    {
        public string MessageId { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string CorrelationId { get; set; }
        public BrokerMessageDeliveryModeEnum DeliveryMode { get; set; }
        public string AppId { get; set; }
    }
}