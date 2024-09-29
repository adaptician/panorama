namespace Panorama.Backing.Dead.Shared.Scenes.Requests.Eto
{
    public class UpdateSceneCommandedEto : IUpdateScene
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}