using MediatR;
using Panorama.Backing.Shared.Common;

namespace Panorama.Backing.Shared.Scenes.Requests.Mediations
{
    public class ScenesOperation : IOperation, IRequest
    {
        public string Operation { get; set; }
        
        public string Data { get; set; }
    }
}