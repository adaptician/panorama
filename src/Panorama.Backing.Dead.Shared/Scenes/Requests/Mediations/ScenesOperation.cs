using MediatR;
using Panorama.Backing.Dead.Shared.Common;

namespace Panorama.Backing.Dead.Shared.Scenes.Requests.Mediations
{
    public class ScenesOperation : IOperation, IRequest
    {
        public string Operation { get; set; }
        
        public string Data { get; set; }
    }
}