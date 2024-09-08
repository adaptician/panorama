namespace Panorama.Delta.Shared.Producers;

public interface ICauseProducer
{
    public void SendMessage<TCause>(TCause message);
}