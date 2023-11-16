using Application.Abstractions;
using Contracts.Abstractions.Protobuf;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Contracts.Shopping.Queries;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class ShoppingCartGrpcService(IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails> getPaymentMethodDetailsInteractor,
        IInteractor<Query.GetShoppingCartDetails, Projection.ShoppingCartDetails> getShoppingCartDetailsInteractor,
        IInteractor<Query.GetCustomerShoppingCartDetails, Projection.ShoppingCartDetails> getCustomerShoppingCartDetailsInteractor,
        IInteractor<Query.GetShoppingCartItemDetails, Projection.ShoppingCartItemDetails> getShoppingCartItemDetailsInteractor,
        IPagedInteractor<Query.ListPaymentMethodsListItems, Projection.PaymentMethodListItem> listPaymentMethodsListItemsInteractor,
        IPagedInteractor<Query.ListShoppingCartItemsListItems, Projection.ShoppingCartItemListItem> listShoppingCartItemsListItemsInteractor)
    : ShoppingQueryService.ShoppingQueryServiceBase()
{
    private readonly IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails> _getPaymentMethodDetailsInteractor = getPaymentMethodDetailsInteractor;

    public override  Task<GetResponse> GetPaymentMethodDetails(GetPaymentMethodDetailsRequest request, ServerCallContext context)
    {
        // var response = await mediator
        //     .CreateRequestClient<Query.GetPaymentMethodDetails>()
        //     .GetResponse<Projection.PaymentMethodDetails, NotFound>(request, context.CancellationToken);
        //
        // return response.Message switch
        // {
        //     Projection.PaymentMethodDetails projection => new() { Projection = Any.Pack((PaymentMethodDetails)projection) },
        //     NotFound _ => new() { NotFound = new() },
        //     _ => throw new InvalidOperationException()
        // };
        
        return base.GetPaymentMethodDetails(request, context);
    }

    public override async Task<GetResponse> GetShoppingCartDetails(GetShoppingCartDetailsRequest request, ServerCallContext context)
    {
        var shoppingCartDetails = await getShoppingCartDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return shoppingCartDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((ShoppingCartDetails)shoppingCartDetails) };
    }

    public override async Task<GetResponse> GetCustomerShoppingCartDetails(GetCustomerShoppingCartDetailsRequest request, ServerCallContext context)
    {
        var shoppingCartDetails = await getCustomerShoppingCartDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return shoppingCartDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((ShoppingCartDetails)shoppingCartDetails) };
    }

    public override async Task<GetResponse> GetShoppingCartItemDetails(GetShoppingCartItemDetailsRequest request, ServerCallContext context)
    {
        var shoppingCartItemDetails = await getShoppingCartItemDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return shoppingCartItemDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((ShoppingCartItemDetails)shoppingCartItemDetails) };
    }

    public override async Task<ListResponse> ListPaymentMethodsListItems(ListPaymentMethodsListItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await listPaymentMethodsListItemsInteractor.InteractAsync(request, context.CancellationToken);

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
        var pagedResult = await listShoppingCartItemsListItemsInteractor.InteractAsync(request, context.CancellationToken);

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