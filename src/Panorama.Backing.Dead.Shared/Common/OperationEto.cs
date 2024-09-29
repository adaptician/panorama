using Panorama.Backing.Dead.Shared.Messages;

namespace Panorama.Backing.Dead.Shared.Common
{
    public class OperationEto : BrokerMessage, IOperation
    {
        public string Operation { get; set; }
        
        public string Data { get; set; }
    }
}