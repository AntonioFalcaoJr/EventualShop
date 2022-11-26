using Application.Abstractions;
using Contracts.Services.Account;
using Contracts.Services.Account.Grpc;
using Grpc.Core;

namespace GrpcService;

public class AccountGrpcService : AccountService.AccountServiceBase
{
    private readonly IInteractor<Query.GetAccount, Projection.AccountDetails> _interactor;

    public AccountGrpcService(IInteractor<Query.GetAccount, Projection.AccountDetails> interactor)
    {
        _interactor = interactor;
    }

    public override async Task<AccountResponse> GetAccount(GetAccountRequest request, ServerCallContext context)
        => await _interactor.InteractAsync(new(new(request.Id)), context.CancellationToken);
}