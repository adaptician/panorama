using Panorama.Backing.Bus.Shared.Common.Eto;

namespace Panorama.Backing.Bus.Shared.Scenes;

public record ScenesRequestedEto : Eto
{
    public string Keyword { get; init; }
        
    public int MaxResultCount { get; init; }
        
    public int SkipCount { get; init; }
}