namespace Panorama.Backing.Shared.Consumers
{
    public interface IConsumer<TPayload>
    {
        void StartConsuming();

        void StopConsuming();

        void Dispose();
    }
}