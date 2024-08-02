using MediatR;
using Panorama.Communication.Shared.Causation;

namespace Panorama.Functions.Causation.Handlers.Contracts;

public class PotentialReceived<TCause> : IRequest
    where TCause : ICause
{
    public TCause Cause { get; set; }
}