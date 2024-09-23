namespace Teatro.Shared.Scenes.Responses;

public interface IPaginatedScenographyResponse
{
    IEnumerable<IScenography> Result { get; set; }
}