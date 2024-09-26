using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Panorama.Scenes.Dto;
using Teatro.Shared.Bases.Dtos;
using Teatro.Shared.Scenes.Dtos;

namespace Panorama.Scenes;

public interface IScenographyProxy
{
    Task<PagedResultDto<ViewSceneDto>> GetAllAsync(PagedSceneResultRequestDto request,
        CancellationToken cancellationToken);

    Task<ViewSceneDto> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<ViewSceneDto> CreateAsync(CreateSceneDto input, CancellationToken cancellationToken);

    Task<HttpResponseMessage> UpdateAsync(UpdateSceneDto input, CancellationToken cancellationToken);

    Task DeleteAsync(long id, CancellationToken cancellationToken);
}