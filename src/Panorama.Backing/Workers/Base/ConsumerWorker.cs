using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Panorama.Backing.Shared.Consumers;

namespace Panorama.Backing.Workers.Base;

public abstract class ConsumerWorker<TPayload> : IHostedService, IDisposable
{
    private readonly IConsumer<TPayload> _consumer;
    
    protected readonly ILogger<ConsumerWorker<TPayload>> Logger;
    
    protected ConsumerWorker(ILogger<ConsumerWorker<TPayload>> logger, 
        IConsumer<TPayload> consumer)
    {
        _consumer = consumer;
        Logger = logger;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Logger.LogTrace("Starting the service bus queue consumer.");
        _consumer.StartConsuming();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Logger.LogTrace("Stopping the service bus queue consumer.");
        _consumer.StartConsuming();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _consumer.Dispose();
        }
    }
}