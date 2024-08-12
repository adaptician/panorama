namespace Panorama.Causation.Producers;

public interface IRabbitMqProducer
{
    public void SendProductMessage < T > (T message);
}