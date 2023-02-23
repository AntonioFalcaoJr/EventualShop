using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.Account;
using Contracts.Services.Account.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class AccountGrpcService : AccountService.AccountServiceBase
{
    private readonly IInteractor<Query.GetAccountDetails, Projection.AccountDetails> _getAccountDetailsInteractor;
    private readonly IPagedInteractor<Query.ListAccountsDetails, Projection.AccountDetails> _listAccountsDetailsInteractor;
    private readonly IPagedInteractor<Query.ListShippingAddressesListItems, Projection.ShippingAddressListItem> _listShippingAddressesListItemsInteractor;

    public AccountGrpcService(
        IInteractor<Query.GetAccountDetails, Projection.AccountDetails> getAccountDetailsInteractor,
        IPagedInteractor<Query.ListAccountsDetails, Projection.AccountDetails> listAccountsDetailsInteractor,
        IPagedInteractor<Query.ListShippingAddressesListItems, Projection.ShippingAddressListItem> listShippingAddressesListItemsInteractor)
    {
        _getAccountDetailsInteractor = getAccountDetailsInteractor;
        _listAccountsDetailsInteractor = listAccountsDetailsInteractor;
        _listShippingAddressesListItemsInteractor = listShippingAddressesListItemsInteractor;
    }

    public override async Task<GetResponse> GetAccountDetails(GetAccountDetailsRequest request, ServerCallContext context)
    {
        var accountDetails = await _getAccountDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return accountDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((AccountDetails)accountDetails) };
    }

    public override async Task<ListResponse> ListAccountsDetails(ListAccountsDetailsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listAccountsDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((AccountDetails)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }

    public override async Task<ListResponse> ListShippingAddressesListItems(ListShippingAddressesListItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listShippingAddressesListItemsInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((AddressListItem)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }
}