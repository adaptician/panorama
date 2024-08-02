using System.Net;

namespace Panorama.Functions.Causation.Clients.Dto;

public class RestfulResponse<T>
{
    /// <summary>
    /// The complete HttpResponseMessage
    /// </summary>
    public HttpResponseMessage HttpResponseMessage { get; }
    
    /// <summary>
    /// The string HTTP Response body
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// The parsed HTTP Response body 
    /// </summary>
    public T? Data { get; set; }
    
    /// <summary>
    /// When set it will contain more information on the specific error
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    public bool IsSuccessStatusCode => HttpResponseMessage.IsSuccessStatusCode;
    
    public HttpStatusCode StatusCode => HttpResponseMessage.StatusCode;

    /// <summary>
    /// Initializes a new instance of the <see cref="RestfulResponse{T}" /> class.
    /// </summary>
    /// <param name="httpResponse">The actual HttpResponseMessage</param>
    /// <param name="data">Data (parsed HTTP body)</param>
    public RestfulResponse(HttpResponseMessage httpResponse, T data)
    {
        HttpResponseMessage = httpResponse ?? throw new ArgumentNullException(nameof(httpResponse));
        Data = data;
        ErrorMessage = string.Empty;
    }

    public RestfulResponse(HttpResponseMessage httpResponse, string errorMessage)
    {
        HttpResponseMessage = httpResponse ?? throw new ArgumentNullException(nameof(httpResponse));
        Data = default;
        ErrorMessage = errorMessage;
        
        if (!IsSuccessStatusCode && string.IsNullOrEmpty(ErrorMessage))
        {
            ErrorMessage = HttpResponseMessage.ReasonPhrase;
        }
    }
    
    public RestfulResponse(HttpResponseMessage httpResponse)
    {
        HttpResponseMessage = httpResponse ?? throw new ArgumentNullException(nameof(httpResponse));
        Data = default;
        ErrorMessage = string.Empty;
    }
}