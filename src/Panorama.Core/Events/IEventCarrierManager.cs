using Abp.Domain.Services;
using Panorama.Events.Errors;

namespace Panorama.Events;

public interface IEventCarrierManager : IDomainService
{
    ErroredCarrier CreateErroredCarrier();
}