using Application.Abstractions;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.Identity;
using Contracts.Services.Identity.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class IdentityGrpcService : IdentityService.IdentityServiceBase
{
    private readonly IInteractor<Query.Login, Projection.UserDetails> _interactor;

    public IdentityGrpcService(IInteractor<Query.Login, Projection.UserDetails> interactor)
    {
        _interactor = interactor;
    }

    public override async Task<GetResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var userDetails = await _interactor.InteractAsync(request, context.CancellationToken);
        
        return userDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((GetUserDetailsResponse)userDetails) };
    } 
}