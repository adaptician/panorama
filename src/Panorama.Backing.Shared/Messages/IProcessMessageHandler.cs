using System.Threading.Tasks;

namespace Panorama.Backing.Shared.Messages
{
    /// <summary>
    /// Define a service bus processing handler for consumed messages. 
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public interface IProcessMessageHandler<in TPayload>
    {
        /// <summary>
        /// Perform processing on a service bus message.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task ProcessMessageAsync(TPayload request);
    }
}