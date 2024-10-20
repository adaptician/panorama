using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos.RetrieveScenes;

public interface IRetrieveScenesXto : IMessageXto
{
    string Keyword { get; }
        
    int MaxResultCount { get; }
        
    int SkipCount { get; }
}