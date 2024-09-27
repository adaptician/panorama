using Panorama.Common.Attributes;

namespace Panorama.Common.Enums
{
    public enum EventBusExchangeTypeEnum
    {
        [Code("fanout")]
        Fanout = 1,
        [Code("direct")]
        Direct,
        [Code("topic")]
        Topic,
        [Code("headers")]
        Headers
    }
}