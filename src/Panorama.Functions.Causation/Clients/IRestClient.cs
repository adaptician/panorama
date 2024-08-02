using Panorama.Functions.Causation.Clients.Dto;

namespace Panorama.Functions.Causation.Clients;

public interface IRestClient
{
    void SetBasicAuth(string username, string password);
    
    void SetBearerToken(string token);

    Task<RestfulResponse<T>> PostAsync<T>(string url, object data, CancellationToken cancellationToken);
}