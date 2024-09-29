using System;

namespace Panorama.Backing.Dead.Shared.Messages
{
    public class BrokerMessage : IBrokerMessage
    {
        public string MessageId { get; set; } = Guid.NewGuid().ToString();
        public string TenantCorrelationId { get; set; }
        public string TenantId { get; set; }
        public string UserCorrelationId { get; set; }
        public string UserId { get; set; }
        public string CorrelationId { get; set; }
        public BrokerMessageDeliveryModeEnum DeliveryMode { get; set; } = BrokerMessageDeliveryModeEnum.NonPersistent;
        public string AppId { get; set; }
    }
}