using System.Threading;
using System.Threading.Tasks;
using Abp.Application.Services;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Scenes.Dto;

namespace Panorama.Scenes;

public interface ISceneAppService : IApplicationService
{
    Task CommandGetAllScenes(PagedSceneResultRequestDto request, CancellationToken cancellationToken);

    Task CommandGetSceneById(string correlationId, CancellationToken cancellationToken);

    Task CommandCreateScene(CreateSceneDto input, CancellationToken cancellationToken);

    Task CommandUpdateScene(UpdateSceneDto input, CancellationToken cancellationToken);

    Task CommandDeleteScene(string correlationId, CancellationToken cancellationToken);
}