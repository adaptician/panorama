using Newtonsoft.Json;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Events.Base;

namespace Panorama.Events.Errors;

public class ErroredEventData : AppEventData
{
    [JsonProperty("error")]
    public ErrorDto Error { get; set; }
}