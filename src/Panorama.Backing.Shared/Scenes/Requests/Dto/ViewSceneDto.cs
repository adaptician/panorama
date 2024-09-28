namespace Panorama.Backing.Shared.Scenes.Requests.Dto
{
    public class ViewSceneDto : IViewScene
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ScenographyId { get; set; }
        public string SceneData { get; set; }
    }
}