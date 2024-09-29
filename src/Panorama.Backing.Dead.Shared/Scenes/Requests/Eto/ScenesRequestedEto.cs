namespace Panorama.Backing.Dead.Shared.Scenes.Requests.Eto
{
    public class ScenesRequestedEto : IRequestScenes
    {
        public string Keyword { get; set; }
        
        public int MaxResultCount { get; set; }
        
        public int SkipCount { get; set; }
    }
}