using System.Net;
using Newtonsoft.Json;
using Panorama.Functions.Causation.Clients.Dto;

namespace Panorama.Functions.Causation.Common;

public static class MockingHelper
{
    public static Task<RestfulResponse<T>> GetMockedApiClientResponse<T>(T data)
    {
        var apiResponse = new RestfulResponse<T>(new HttpResponseMessage(HttpStatusCode.OK), data)
        {
            Content = JsonConvert.SerializeObject(data)
        };

        return Task.FromResult(apiResponse);
    }
}