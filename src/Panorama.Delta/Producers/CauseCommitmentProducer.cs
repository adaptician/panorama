using Panorama.Delta.Shared.Producers;

namespace Panorama.Causation.Producers;

public class CauseCommitmentProducer : ICauseProducer
{
    public void SendMessage<T>(T message)
    {
        throw new NotImplementedException("TODO: implement CAUSE COMMITMENT RabbitMQ Producer (3).");
        // create topic: caused.*
        // submit payload to bus topic
    }
}