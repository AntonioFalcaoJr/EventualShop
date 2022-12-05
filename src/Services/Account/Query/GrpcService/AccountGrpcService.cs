using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Account;
using Contracts.Services.Account.Protobuf;
using Grpc.Core;

namespace GrpcService;

public class AccountGrpcService : AccountService.AccountServiceBase
{
    private readonly IInteractor<Query.ListAccounts, IPagedResult<Projection.AccountDetails>> _listAccountsInteractor;
    private readonly IInteractor<Query.ListShippingAddresses, IPagedResult<Projection.ShippingAddressListItem>> _listAddressesInteractor;
    private readonly IInteractor<Query.GetAccount, Projection.AccountDetails> _getAccountInteractor;

    public AccountGrpcService(
        IInteractor<Query.GetAccount, Projection.AccountDetails> getAccountInteractor,
        IInteractor<Query.ListAccounts, IPagedResult<Projection.AccountDetails>> listAccountsInteractor,
        IInteractor<Query.ListShippingAddresses, IPagedResult<Projection.ShippingAddressListItem>> listAddressesInteractor)
    {
        _listAccountsInteractor = listAccountsInteractor;
        _listAddressesInteractor = listAddressesInteractor;
        _getAccountInteractor = getAccountInteractor;
    }

    public override async Task<Account> GetAccount(GetAccountRequest request, ServerCallContext context)
        => await _getAccountInteractor.InteractAsync(request, context.CancellationToken);

    public override async Task<Accounts> ListAccounts(ListAccountsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listAccountsInteractor.InteractAsync(request, context.CancellationToken);

        return new()
        {
            Items = { pagedResult.Items.Select(details => (Account)details) },
            Page = new()
            {
                Current = pagedResult.Page.Current,
                Size = pagedResult.Page.Size,
                HasNext = pagedResult.Page.HasNext,
                HasPrevious = pagedResult.Page.HasPrevious
            }
        };
    }

    public override async Task<Addresses> ListShippingAddresses(ListShippingAddressesRequest request, ServerCallContext context)
    {
        var pagedResult = await _listAddressesInteractor.InteractAsync(request, context.CancellationToken);

        return new()
        {
            Items = { pagedResult.Items.Select(details => (Address)details) },
            Page = new()
            {
                Current = pagedResult.Page.Current,
                Size = pagedResult.Page.Size,
                HasNext = pagedResult.Page.HasNext,
                HasPrevious = pagedResult.Page.HasPrevious
            }
        };
    }
}