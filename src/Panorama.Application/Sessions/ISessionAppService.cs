using System.Threading.Tasks;
using Abp.Application.Services;
using Panorama.Sessions.Dto;

namespace Panorama.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
