using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos.RequestScenes;

public interface IRequestScenesXto : IMessageXto
{
    string Keyword { get; }
        
    int MaxResultCount { get; }
        
    int SkipCount { get; }
}