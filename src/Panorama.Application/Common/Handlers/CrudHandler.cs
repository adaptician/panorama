// using System.Threading;
// using System.Threading.Tasks;
// using Abp.Runtime.Session;
// using MediatR;
// using Panorama.Backing.Dead.Producers;
// using Panorama.Backing.Dead.Shared.Common;
// using Panorama.Backing.Dead.Shared.Messages;
// using Panorama.Common.Extensions;
// using Panorama.Common.Mediations;
//
// namespace Panorama.Common.Handlers;
//
// public abstract class CrudHandler<TRequest, TRequestEto>(ScenesProducer producer) 
//     : PanoramaAppServiceBase, IRequestHandler<TRequest>
// where TRequest : IRequest
// where TRequestEto : IBrokerMessage, IOperation
// {
//     protected abstract string RoutingKey { get; }
//     
//     public async Task Handle(TRequest request, CancellationToken cancellationToken)
//     {
//         Logger.Trace($"A request to retrieve Scenes was received. " +
//                      $"Request {MediationActionEnum.Received.GetCode()}");
//         
//         if (request == null)
//         {
//             Logger.Warn($"A request to retrieve Scenes was received as invalid." +
//                         $"Request {MediationActionEnum.Ignored.GetCode()}");
//
//             return; // Go no further.
//         }
//
//         var userId = AbpSession.GetUserId();
//         var user = await UserManager.GetUserByIdAsync(userId);
//
//         var requestEto = ObjectMapper.Map<TRequestEto>(request);
//         requestEto.UserCorrelationId = user.CorrelationId;
//         requestEto.UserId = user.Id.ToString();
//         requestEto.TenantId = user.TenantId.ToString();
//         
//         producer.PublishMessage(requestEto, RoutingKey);
//         
//         Logger.Trace($"A request to retrieve Scenes was published. " +
//                      $"Request {MediationActionEnum.Published.GetCode()}");
//     }
// }