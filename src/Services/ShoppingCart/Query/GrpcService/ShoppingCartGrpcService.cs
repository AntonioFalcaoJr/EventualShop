using Application.Abstractions;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.ShoppingCart.Protobuf;
using Contracts.Services.ShoppingCart;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class ShoppingCartGrpcService : ShoppingCartService.ShoppingCartServiceBase
{
    private readonly IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails> _getPaymentMethodDetailsInteractor;
    private readonly IInteractor<Query.GetShoppingCartDetails, Projection.ShoppingCartDetails> _getShoppingCartDetailsInteractor;
    private readonly IInteractor<Query.GetCustomerShoppingCartDetails, Projection.ShoppingCartDetails> _getCustomerShoppingCartDetailsInteractor;
    private readonly IInteractor<Query.GetShoppingCartItemDetails, Projection.ShoppingCartItemDetails> _getShoppingCartItemDetailsInteractor;
    private readonly IPagedInteractor<Query.ListPaymentMethodsListItems, Projection.PaymentMethodListItem> _listPaymentMethodsListItemsInteractor;
    private readonly IPagedInteractor<Query.ListShoppingCartItemsListItems, Projection.ShoppingCartItemListItem> _listShoppingCartItemsListItemsInteractor;

    public ShoppingCartGrpcService(
        IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails> getPaymentMethodDetailsInteractor,
        IInteractor<Query.GetShoppingCartDetails, Projection.ShoppingCartDetails> getShoppingCartDetailsInteractor,
        IInteractor<Query.GetCustomerShoppingCartDetails, Projection.ShoppingCartDetails> getCustomerShoppingCartDetailsInteractor,
        IInteractor<Query.GetShoppingCartItemDetails, Projection.ShoppingCartItemDetails> getShoppingCartItemDetailsInteractor,
        IPagedInteractor<Query.ListPaymentMethodsListItems, Projection.PaymentMethodListItem> listPaymentMethodsListItemsInteractor,
        IPagedInteractor<Query.ListShoppingCartItemsListItems, Projection.ShoppingCartItemListItem> listShoppingCartItemsListItemsInteractor)
    {
        _getPaymentMethodDetailsInteractor = getPaymentMethodDetailsInteractor;
        _getShoppingCartDetailsInteractor = getShoppingCartDetailsInteractor;
        _getCustomerShoppingCartDetailsInteractor = getCustomerShoppingCartDetailsInteractor;
        _getShoppingCartItemDetailsInteractor = getShoppingCartItemDetailsInteractor;
        _listPaymentMethodsListItemsInteractor = listPaymentMethodsListItemsInteractor;
        _listShoppingCartItemsListItemsInteractor = listShoppingCartItemsListItemsInteractor;
    }

    public override async Task<GetResponse> GetPaymentMethodDetails(GetPaymentMethodDetailsRequest request, ServerCallContext context)
    {
        var paymentMethodDetails = await _getPaymentMethodDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return paymentMethodDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((PaymentMethodDetails)paymentMethodDetails) };
    }

    public override async Task<GetResponse> GetShoppingCartDetails(GetShoppingCartDetailsRequest request, ServerCallContext context)
    {
        var shoppingCartDetails = await _getShoppingCartDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return shoppingCartDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((ShoppingCartDetails)shoppingCartDetails) };
    }

    public override async Task<GetResponse> GetCustomerShoppingCartDetails(GetCustomerShoppingCartDetailsRequest request, ServerCallContext context)
    {
        var shoppingCartDetails = await _getCustomerShoppingCartDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return shoppingCartDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((ShoppingCartDetails)shoppingCartDetails) };
    }

    public override async Task<GetResponse> GetShoppingCartItemDetails(GetShoppingCartItemDetailsRequest request, ServerCallContext context)
    {
        var shoppingCartItemDetails = await _getShoppingCartItemDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return shoppingCartItemDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((ShoppingCartItemDetails)shoppingCartItemDetails) };
    }

    public override async Task<ListResponse> ListPaymentMethodsListItems(ListPaymentMethodsListItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listPaymentMethodsListItemsInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((PaymentMethodListItem)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }

    public override async Task<ListResponse> ListShoppingCartItemsListItems(ListShoppingCartItemsListItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listShoppingCartItemsListItemsInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((ShoppingCartItemListItem)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }
}