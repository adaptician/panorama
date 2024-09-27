using System.Threading;
using System.Threading.Tasks;
using Abp.Application.Services;
using Panorama.Scenes.Dto;
using Teatro.Shared.Scenes.Dtos;

namespace Panorama.Scenes;

public interface ISceneAppService : IApplicationService
{
    Task GetAll(PagedSceneResultRequestDto request, CancellationToken cancellationToken);

    Task GetById(long id, CancellationToken cancellationToken);

    Task Create(CreateSceneDto input, CancellationToken cancellationToken);

    Task Update(UpdateSceneDto input, CancellationToken cancellationToken);

    Task Delete(long id, CancellationToken cancellationToken);
}