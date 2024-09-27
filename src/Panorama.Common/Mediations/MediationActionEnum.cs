using Panorama.Common.Attributes;

namespace Panorama.Common.Mediations
{
    public enum MediationActionEnum
    {
        [Code("RECEIVED")]
        Received = 10,
        [Code("ACCEPTED")]
        Accepted = 20,
        
        
        [Code("IGNORED")]
        Ignored = 70,
    }
}