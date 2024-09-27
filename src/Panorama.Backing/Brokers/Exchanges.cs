using Panorama.Backing.Common;
using Panorama.Common.Enums;
using Panorama.Common.Extensions;

namespace Panorama.Backing.Brokers;

public abstract class Exchanges : ReflectToDictionary<string, string>
{
    public static class ExchangeNames
    {
        public const string ScenesExchange = "scenes";
    }
    
    public static class DefinedExchanges
    {
        public static (string, string) ScenesExchange = 
            (ExchangeNames.ScenesExchange, EventBusExchangeTypeEnum.Direct.GetCode());
    }
    
    public static Dictionary<string, string> GetAllExchanges()
    {
        return GetAll([typeof(DefinedExchanges)]);
    }
}