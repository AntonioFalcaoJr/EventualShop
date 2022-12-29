using Application.Abstractions;
using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.Catalog;
using Contracts.Services.Catalog.Protobuf;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class CatalogGrpcService : CatalogService.CatalogServiceBase
{
    private readonly IInteractor<Query.GetCatalogItemDetails, Projection.CatalogItemDetails> _getCatalogItemDetailsInteractor;
    private readonly IInteractor<Query.ListCatalogsGridItems, IPagedResult<Projection.CatalogGridItem>> _listCatalogsGridItemsInteractor;
    private readonly IInteractor<Query.ListCatalogItemsCards, IPagedResult<Projection.CatalogItemCard>> _listCatalogItemsCardsInteractor;
    private readonly IInteractor<Query.ListCatalogItemsListItems, IPagedResult<Projection.CatalogItemListItem>> _listCatalogItemsListItemsInteractor;

    public CatalogGrpcService(
        IInteractor<Query.GetCatalogItemDetails, Projection.CatalogItemDetails> getCatalogItemDetailsInteractor,
        IInteractor<Query.ListCatalogItemsCards, IPagedResult<Projection.CatalogItemCard>> listCatalogItemsCardsInteractor,
        IInteractor<Query.ListCatalogsGridItems, IPagedResult<Projection.CatalogGridItem>> listCatalogsGridItemsInteractor,
        IInteractor<Query.ListCatalogItemsListItems, IPagedResult<Projection.CatalogItemListItem>> listCatalogItemsListItemsInteractor)
    {
        _getCatalogItemDetailsInteractor = getCatalogItemDetailsInteractor;
        _listCatalogItemsCardsInteractor = listCatalogItemsCardsInteractor;
        _listCatalogsGridItemsInteractor = listCatalogsGridItemsInteractor;
        _listCatalogItemsListItemsInteractor = listCatalogItemsListItemsInteractor;
    }

    public override async Task<GetResponse> GetCatalogItemDetails(GetCatalogItemDetailsRequest request, ServerCallContext context)
    {
        var itemDetails = await _getCatalogItemDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return itemDetails is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((CatalogItemDetails)itemDetails) };
    }

    public override async Task<ListResponse> ListCatalogsGridItems(ListCatalogsGridItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listCatalogsGridItemsInteractor.InteractAsync(request, context.CancellationToken);
        
        return pagedResult!.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((CatalogGridItem)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }

    public override async Task<ListResponse> ListCatalogItemsListItems(ListCatalogItemsListItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listCatalogItemsListItemsInteractor.InteractAsync(request, context.CancellationToken);
        
        return pagedResult!.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((CatalogItemListItem)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }

    public override async Task<ListResponse> ListCatalogItemsCards(ListCatalogItemsCardsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listCatalogItemsCardsInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult!.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((CatalogItemCard)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }
}