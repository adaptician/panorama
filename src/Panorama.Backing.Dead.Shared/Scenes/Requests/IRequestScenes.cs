namespace Panorama.Backing.Dead.Shared.Scenes.Requests
{
    public interface IRequestScenes
    {
        string Keyword { get; set; }
        
        int MaxResultCount { get; set; }
        
        int SkipCount { get; set; }
    }
}