namespace Panorama.Backing.Bus.Shared.Scenes;

public record ScenesRequestedEto
{
    Guid CommandId { get; }
    
    DateTime Timestamp { get; }
    
    public string Keyword { get; init; }
        
    public int MaxResultCount { get; init; }
        
    public int SkipCount { get; init; }
}