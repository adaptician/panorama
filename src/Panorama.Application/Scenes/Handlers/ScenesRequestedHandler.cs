using System.Threading;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using MediatR;
using Panorama.Backing.Shared.Scenes.Requests;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Common.Extensions;
using Panorama.Common.Mediations;

namespace Panorama.Scenes.Handlers;

public class ScenesRequestedHandler : PanoramaAppServiceBase, IRequestHandler<ScenesRequested>
{
    public async Task Handle(ScenesRequested request, CancellationToken cancellationToken)
    {
        Logger.Info($"A request to retrieve Scenes was received. " +
                    $"Request {MediationActionEnum.Received.GetCode()}");
        
        if (request == null)
        {
            Logger.Warn($"A request to retrieve Scenes was received as invalid." +
                        $"Request {MediationActionEnum.Ignored.GetCode()}");

            return; // Go no further.
        }

        var userId = AbpSession.GetUserId();
        var user = await UserManager.GetUserByIdAsync(userId);

        var requestEto = ObjectMapper.Map<ScenesRequestedEto>(request);
        requestEto.UserCorrelationId = user.CorrelationId;
    }
}