using Teatro.Shared.Scenes.Requests;
using Teatro.Shared.Scenes.Responses;

namespace Teatro.Shared.Scenes;

/// <summary>
/// A Scenography service is a CRUD service.
/// Scene payloads are accepted through the service and persisted into a store.
/// </summary>
public interface IScenographyService
{
    Task<IPaginatedScenographyResponse> Get(IFilterScenographyRequest request);

    Task<IScenography> Get(long id);

    Task Simulate(long id);

    Task Join(long id);

    Task Leave(long id);
    
    Task<long> Create(ICreateScenographyRequest request);

    Task<long> Update(IUpdateScenographyRequest request);

    Task Delete(long id);
}