using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Extensions;
using Castle.Core.Logging;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Panorama.Options;
using Panorama.Scenes.Dto;
using Teatro.Shared.Bases.Dtos;
using Teatro.Shared.Scenes.Dtos;

namespace Panorama.Scenes;

// TODO: Fix this
// This is an antipattern. Awaiting HTTP sync to a microservice is an antipattern because if something goes down,
// the system is rendered into an error state.
// Even for fetching queries.
// This is the challenge and cost of microservices, is that by design the architecture must be decoupled.
// By design the consumers must listen for updates, but not await them.

// When the service bus in place, this needs to be replaced by MediatR, to request data.
// And the response needs to be sent via SignalR.
// The only problem is if SignalR fails? Well, reload will trigger the next request.
// This can continue until messages are consumed, it makes no difference.
public class ScenographyProxy : IScenographyProxy
{
    private readonly HttpClient _httpClient;
    private readonly ScenographyProxyOptions _proxyOptions;
    private readonly ILogger _logger;

    public ScenographyProxy(HttpClient httpClient, 
        IOptions<ScenographyProxyOptions> proxyOptions, 
        ILogger logger)
    {
        _httpClient = httpClient;
        _proxyOptions = proxyOptions.Value;
        _logger = logger;
        
        // TODO: secret should go onto header as bearer token
    }
    
    public async Task<PagedResultDto<ViewSceneDto>> GetAllAsync(PagedSceneResultRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var proxyUrl = _proxyOptions.BaseUrl.EnsureEndsWith('/') 
                           + ScenographyConstants.Endpoint;
            
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                parameters.Add(nameof(request.Keyword), request.Keyword);
            }
            
            parameters.Add(nameof(request.MaxResultCount), request.MaxResultCount.ToString());
            parameters.Add(nameof(request.SkipCount), request.SkipCount.ToString());
            
            var uri = QueryHelpers.AddQueryString(proxyUrl, parameters);

            var response = await _httpClient.GetAsync(uri, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"The request failed with reason: {response.ReasonPhrase}");
            }
            
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<PagedResultDto<ViewSceneDto>>(responseContent);

        }
        catch (Exception e)
        {
            _logger.Error($"Error making proxy call to {nameof(ScenographyProxy)} {nameof(GetAllAsync)}.", e);
            throw;
        }
    }
    
    public async Task<ViewSceneDto> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var proxyUrl = _proxyOptions.BaseUrl.EnsureEndsWith('/') 
                           + ScenographyConstants.Endpoint;

            var uri = $"{proxyUrl}/{nameof(id)}={id}";

            var response = await _httpClient.GetAsync(uri, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"The request failed with reason: {response.ReasonPhrase}");
            }
            
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<ViewSceneDto>(responseContent);

        }
        catch (Exception e)
        {
            _logger.Error($"Error making proxy call to {nameof(ScenographyProxy)} {nameof(GetByIdAsync)}.", e);
            throw;
        }
    }

    public async Task<ViewSceneDto> CreateAsync(CreateSceneDto input, CancellationToken cancellationToken)
    {
        try
        {
            var proxyUrl = _proxyOptions.BaseUrl.EnsureEndsWith('/')
                           + ScenographyConstants.Endpoint;

            var uri = $"{proxyUrl}";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, jsonContent, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"The request failed with reason: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonConvert.DeserializeObject<ViewSceneDto>(responseContent);

        }
        catch (Exception e)
        {
            _logger.Error($"Error making proxy call to {nameof(ScenographyProxy)} {nameof(CreateAsync)}.", e);
            throw;
        }
    }
    
    public async Task<HttpResponseMessage> UpdateAsync(UpdateSceneDto input, CancellationToken cancellationToken)
    {
        try
        {
            var proxyUrl = _proxyOptions.BaseUrl.EnsureEndsWith('/')
                           + ScenographyConstants.Endpoint;

            var uri = $"{proxyUrl}";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, jsonContent, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"The request failed with reason: {response.ReasonPhrase}");
            }

            return response;

        }
        catch (Exception e)
        {
            _logger.Error($"Error making proxy call to {nameof(ScenographyProxy)} {nameof(UpdateAsync)}.", e);
            throw;
        }
    }
    
    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var proxyUrl = _proxyOptions.BaseUrl.EnsureEndsWith('/') 
                           + ScenographyConstants.Endpoint;

            var uri = $"{proxyUrl}/{nameof(id)}={id}";

            var response = await _httpClient.DeleteAsync(uri, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"The request failed with reason: {response.ReasonPhrase}");
            }

        }
        catch (Exception e)
        {
            _logger.Error($"Error making proxy call to {nameof(ScenographyProxy)} {nameof(DeleteAsync)}.", e);
            throw;
        }
    }

    private class ScenographyConstants
    {
        internal const string SecretKeyParam = "sct";
        
        internal const string Endpoint = "scene";
    }
}