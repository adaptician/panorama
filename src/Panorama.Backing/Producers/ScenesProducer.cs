using Microsoft.Extensions.Logging;
using Panorama.Backing.Brokers;
using Panorama.Backing.ConnectionPools;
using Panorama.Backing.Producers.Base;

namespace Panorama.Backing.Producers;

public class ScenesProducer : Producer
{
    private const string Exchange = Exchanges.ExchangeNames.ScenesExchange;

    public ScenesProducer(ILogger<Producer> logger, 
        IRabbitMqConnectionPool connectionPool) 
        : base(Exchange, logger, connectionPool)
    {
    }
}