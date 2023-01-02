using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Account;
using Contracts.Services.Account.Protobuf;

namespace GrpcService;

public class AccountGrpcService : AccountService.AccountServiceBase
{
    private readonly IInteractor<Query.GetAccountDetails, Projection.AccountDetails> _getAccountDetailsInteractor;
    private readonly IInteractor<Query.ListAccountsDetails, IPagedResult<Projection.AccountDetails>> _listAccountsDetailsInteractor;
    private readonly IInteractor<Query.ListShippingAddressesListItems, IPagedResult<Projection.ShippingAddressListItem>> _listShippingAddressesListItemsInteractor;

    public AccountGrpcService(
        IInteractor<Query.GetAccountDetails, Projection.AccountDetails> getAccountDetailsInteractor,
        IInteractor<Query.ListAccountsDetails, IPagedResult<Projection.AccountDetails>> listAccountsDetailsInteractor,
        IInteractor<Query.ListShippingAddressesListItems, IPagedResult<Projection.ShippingAddressListItem>> listShippingAddressesListItemsInteractor)
    {
        _getAccountDetailsInteractor = getAccountDetailsInteractor;
        _listAccountsDetailsInteractor = listAccountsDetailsInteractor;
        _listShippingAddressesListItemsInteractor = listShippingAddressesListItemsInteractor;
    }
    
}