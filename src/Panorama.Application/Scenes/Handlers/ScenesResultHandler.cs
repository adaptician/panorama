// using System;
// using System.Threading.Tasks;
// using Abp;
// using Abp.Application.Services.Dto;
// using Abp.Domain.Services;
// using Abp.Domain.Uow;
// using AutoMapper;
// using AutoMapper.Internal.Mappers;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Logging;
// using Newtonsoft.Json;
// using Panorama.Authorization.Users;
// using Panorama.Backing.Dead.Shared.Messages;
// using Panorama.Backing.Dead.Shared.Scenes.Requests.Dto;
// using Panorama.Backing.Dead.Shared.Scenes.Requests.Eto;
// using Panorama.Common.Extensions;
// using Panorama.Common.Mediations;
// using Panorama.Scenes.Events.ScenesReceived;
//
// namespace Panorama.Scenes.Handlers;
//
// public class ScenesResultHandler(
//     ILogger<ScenesResultHandler> logger,
//     ISceneManager sceneManager
//     ) : DomainService, IProcessMessageHandler<ScenesResultEto>
// {
//     public async Task ProcessMessageAsync(ScenesResultEto request)
//     {
//         logger.LogTrace($"A request to process {nameof(ScenesResultEto)} was received. " +
//                         $"Request {MediationActionEnum.Received.GetCode()}");
//         
//         if (request == null || string.IsNullOrEmpty(request.Data))
//         {
//             logger.LogWarning($"A request to process {nameof(ScenesResultEto)} was received as invalid." +
//                               $"Request {MediationActionEnum.Ignored.GetCode()}");
//
//             return; // Go no further.
//         }
//         
//         var data = new ScenesReceivedEventData
//         {
//             Data = JsonConvert.DeserializeObject<PagedResultDto<ViewSceneDto>>(request.Data)
//         };
//         
//         var carrier = sceneManager.CreateScenesReceivedCarrier();
//
//         int? tenantId = ConvertToInt(request.TenantId);
//         long? userId = ConvertToLong(request.UserId);
//
//         if (!userId.HasValue)
//         {
//             throw new Exception("Unable to broadcast message - user id is invalid.");
//         }
//         
//         await carrier.Broadcast(data, new UserIdentifier(tenantId, userId.Value));
//     }
//     
//     static int? ConvertToInt(string input)
//     {
//         if (string.IsNullOrWhiteSpace(input))
//         {
//             return null;
//         }
//
//         if (int.TryParse(input, out int result))
//         {
//             return result;
//         }
//
//         return null;
//     }
//     
//     static long? ConvertToLong(string input)
//     {
//         if (string.IsNullOrWhiteSpace(input))
//         {
//             return null;
//         }
//
//         if (long.TryParse(input, out long result))
//         {
//             return result;
//         }
//
//         return null;
//     }
// }