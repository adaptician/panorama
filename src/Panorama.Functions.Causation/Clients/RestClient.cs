using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Panorama.Functions.Causation.Clients.Dto;

namespace Panorama.Functions.Causation.Clients;

public class RestClient : IRestClient
{
    private readonly HttpClient _httpClient;

    public RestClient(HttpClient httpClient, Uri baseAddress)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        _httpClient.BaseAddress = baseAddress;
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public void SetBasicAuth(string username, string password)
    {
        var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);
    }

    public void SetBearerToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<RestfulResponse<T>> PostAsync<T>(string url, object data,
        CancellationToken cancellationToken = default)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, jsonContent, cancellationToken);

        return await ToRestfulResponse<T>(response, cancellationToken);
    }

    private async Task<RestfulResponse<T>> ToRestfulResponse<T>(HttpResponseMessage payload,
        CancellationToken cancellationToken)
    {
        var result = new RestfulResponse<T>(payload);

        var requestUri = payload.RequestMessage?.RequestUri;

        try
        {
            if (result.IsSuccessStatusCode)
            {
                var responseContent = await payload.Content.ReadAsStringAsync(cancellationToken);
                result.Content = responseContent;

                var data = JsonConvert.DeserializeObject<T>(responseContent);
                result.Data = data;
            }
            else
            {
                var responseContent = await payload.Content.ReadAsStringAsync(cancellationToken);
                result.Content = responseContent;
                result.ErrorMessage = payload.ReasonPhrase;
            }
        }
        catch (JsonException ex)
        {
            result.ErrorMessage =
                $"Failed \"{requestUri}\". Error deserializing response: {payload.ReasonPhrase} - {ex.Message}";
        }

        return result;
    }
}