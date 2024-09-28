using Panorama.Backing.Shared.Messages;

namespace Panorama.Backing.Shared.Common
{
    public class ResultEto : BrokerMessage, IResultEto
    {
        public string Operation { get; set; }
        public string Data { get; set; }
        public bool HasError { get; set; }
        public IErrorEto Error { get; set; }
    }

    public class ErrorEto : IErrorEto
    {
        public string Message { get; set; }
        public string Decision { get; set; }
    }
}