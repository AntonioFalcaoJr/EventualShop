using Application.Abstractions.UseCases;
using Contracts.Query;
using Contracts.Services.Identity;
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
        => await _interactor.InteractAsync(new(request.Email, request.Password), context.CancellationToken);
}