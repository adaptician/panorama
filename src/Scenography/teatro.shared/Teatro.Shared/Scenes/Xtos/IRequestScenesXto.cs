using Teatro.Shared.Common.Xtos;

namespace Teatro.Shared.Scenes.Xtos;

public interface IRequestScenesXto : IMessageXto
{
    string Keyword { get; init; }
        
    int MaxResultCount { get; init; }
        
    int SkipCount { get; init; }
}