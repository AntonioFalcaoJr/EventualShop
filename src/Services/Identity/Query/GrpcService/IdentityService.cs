using Application.Abstractions.UseCases;
using Com.Google.Protobuf;
using Contracts.Services.Identity;
using Grpc.Core;

namespace GrpcService;

public class IdentityService : Com.Google.Protobuf.IdentityService.IdentityServiceBase
{
    private readonly IInteractor<Query.Login, Projection.UserDetails> _interactor;

    public IdentityService(IInteractor<Query.Login, Projection.UserDetails> interactor)
    {
        _interactor = interactor;
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        => await _interactor.InteractAsync(new(request.Email, request.Password), context.CancellationToken);
}