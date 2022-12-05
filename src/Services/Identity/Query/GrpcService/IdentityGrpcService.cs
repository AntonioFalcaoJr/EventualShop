using Application.Abstractions;
using Contracts.Services.Identity;
using Contracts.Services.Identity.Protobuf;
using Grpc.Core;

namespace GrpcService;

public class IdentityGrpcService : IdentityService.IdentityServiceBase
{
    private readonly IInteractor<Query.Login, Projection.UserDetails> _interactor;

    public IdentityGrpcService(IInteractor<Query.Login, Projection.UserDetails> interactor)
    {
        _interactor = interactor;
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        => await _interactor.InteractAsync(request, context.CancellationToken);
}