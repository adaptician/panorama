using Panorama.Backing.Dead.Common;
using Panorama.Common.Enums;
using Panorama.Common.Extensions;

namespace Panorama.Backing.Dead.Brokers;

public abstract class Exchanges : ReflectToDictionary<string, string>
{
    private static class DefinedExchanges
    {
        public static (string, string) ScenesExchange = 
            (ExchangeNames.ScenesExchange, EventBusExchangeTypeEnum.Direct.GetCode());
    }
    
    public static class ExchangeNames
    {
        public const string ScenesExchange = "scenes";
    }
    
    public static Dictionary<string, string> GetAll()
    {
        return GetAll([typeof(DefinedExchanges)]);
    }
}