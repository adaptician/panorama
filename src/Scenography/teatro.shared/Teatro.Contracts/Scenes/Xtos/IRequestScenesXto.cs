using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos;

public interface IRequestScenesXto : IMessageXto
{
    string Keyword { get; init; }
        
    int MaxResultCount { get; init; }
        
    int SkipCount { get; init; }
}