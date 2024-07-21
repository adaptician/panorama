using Abp.Application.Services;
using Panorama.MultiTenancy.Dto;

namespace Panorama.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

