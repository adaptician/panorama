using Panorama.Backing.Shared.Messages;

namespace Panorama.Backing.Shared.Common
{
    public class OperationEto : BrokerMessage, IOperation
    {
        public string Operation { get; set; }
        
        public string Data { get; set; }
    }
}