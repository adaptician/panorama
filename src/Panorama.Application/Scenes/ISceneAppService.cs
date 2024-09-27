using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Abp.Application.Services;
using Panorama.Scenes.Dto;
using Teatro.Shared.Bases.Dtos;
using Teatro.Shared.Scenes.Dtos;

namespace Panorama.Scenes;

public interface ISceneAppService : IApplicationService
{
    Task RequestGetAll(PagedSceneResultRequestDto request, CancellationToken cancellationToken);
    
    // TODO: remove
    Task<PagedResultDto<ViewSceneDto>> GetAll(PagedSceneResultRequestDto request, CancellationToken cancellationToken);

    Task<ViewSceneDto> GetById(long id, CancellationToken cancellationToken);

    Task<ViewSceneDto> Create(CreateSceneDto input, CancellationToken cancellationToken);

    Task Update(UpdateSceneDto input, CancellationToken cancellationToken);

    Task Delete(long id, CancellationToken cancellationToken);
}