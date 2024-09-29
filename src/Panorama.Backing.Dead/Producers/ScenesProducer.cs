using Microsoft.Extensions.Logging;
using Panorama.Backing.Dead.Brokers;
using Panorama.Backing.Dead.ConnectionPools;
using Panorama.Backing.Dead.Producers.Base;

namespace Panorama.Backing.Dead.Producers;

public class ScenesProducer : Producer
{
    private const string Exchange = Exchanges.ExchangeNames.ScenesExchange;

    public ScenesProducer(ILogger<Producer> logger, 
        IRabbitMqConnectionPool connectionPool) 
        : base(Exchange, logger, connectionPool)
    {
    }
}