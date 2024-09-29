namespace Panorama.Backing.Dead.Shared.Consumers
{
    public interface IConsumer<TPayload>
    {
        void StartConsuming();

        void StopConsuming();

        void Dispose();
    }
}