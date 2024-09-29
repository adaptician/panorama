namespace Panorama.Backing.Dead.Shared.Scenes.Requests.Eto
{
    public class CreateSceneCommandedEto : ICreateScene
    {
        public string Name { get; set; }
    
        public string Description { get; set; }
    
        public string InitialSceneData { get; set; }
    }
}