using System.Threading;
using System.Threading.Tasks;
using Abp.Application.Services;
using Panorama.Scenes.Dto;

namespace Panorama.Scenes;

public interface ISceneAppService : IApplicationService
{
    Task GetAll(PagedSceneResultRequestDto request, CancellationToken cancellationToken);

    // Task<ViewSceneDto> GetById(long id, CancellationToken cancellationToken);
    //
    // Task<ViewSceneDto> Create(CreateSceneDto input, CancellationToken cancellationToken);
    //
    // Task Update(UpdateSceneDto input, CancellationToken cancellationToken);
    //
    // Task Delete(long id, CancellationToken cancellationToken);
}