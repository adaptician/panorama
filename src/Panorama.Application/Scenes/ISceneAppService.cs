using System.Threading;
using System.Threading.Tasks;
using Abp.Application.Services;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Scenes.Dto;

namespace Panorama.Scenes;

public interface ISceneAppService : IApplicationService
{
    Task CommandGetAll(PagedSceneResultRequestDto request, CancellationToken cancellationToken);

    Task CommandGetById(string correlationId, CancellationToken cancellationToken);

    Task CommandCreate(CreateSceneDto input, CancellationToken cancellationToken);

    Task CommandUpdate(UpdateSceneDto input, CancellationToken cancellationToken);

    // Task Delete(long id, CancellationToken cancellationToken);
}