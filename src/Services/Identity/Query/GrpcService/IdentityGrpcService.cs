using Application.Abstractions;
using Contracts.Abstractions.Protobuf;
using Contracts.Boundaries.Identity;
using Contracts.Services.Identity.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class IdentityGrpcService(IInteractor<Query.Login, Projection.UserDetails> interactor)
    : IdentityService.IdentityServiceBase
{
    public override async Task<GetResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var userDetails = await interactor.InteractAsync(request, context.CancellationToken);

        return userDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((UserDetails)userDetails) };
    }
}