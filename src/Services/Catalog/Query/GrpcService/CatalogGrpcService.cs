using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;
using Contracts.Services.Catalog.Protobuf;
using Grpc.Core;

namespace GrpcService;

public class CatalogGrpcService : CatalogService.CatalogServiceBase
{
    private readonly IInteractor<Query.GetCatalog, Projection.Catalog> _getCatalogInteractor;
    private readonly IInteractor<Query.GetCatalogItems, Projection.CatalogItem> _getCatalogItemInteractor;
    private readonly IInteractor<Query.GetCatalogs, IPagedResult<Projection.Catalog>> _listCatalogsInteractor;
    private readonly IInteractor<Query.GetAllItems, IPagedResult<Projection.CatalogItem>> _listCatalogItemsInteractor;

    public CatalogGrpcService(
        IInteractor<Query.GetCatalog, Projection.Catalog> getCatalogInteractor,
        IInteractor<Query.GetCatalogItems, Projection.CatalogItem> getCatalogItemInteractor,
        IInteractor<Query.GetCatalogs, IPagedResult<Projection.Catalog>> listCatalogsInteractor,
        IInteractor<Query.GetAllItems, IPagedResult<Projection.CatalogItem>> lstCatalogItemsInteractor
        )
    {
        _getCatalogInteractor = getCatalogInteractor;
        _getCatalogItemInteractor = getCatalogItemInteractor;
        _listCatalogsInteractor = listCatalogsInteractor;
        _listCatalogItemsInteractor = lstCatalogItemsInteractor;
    }


    public override async Task<Catalog> GetCatalog(GetCatalogRequest request, ServerCallContext context)
        => await _getCatalogInteractor.InteractAsync(request, context.CancellationToken);
    
    public override async Task<Catalogs> ListCatalogs(ListCatalogsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listCatalogsInteractor.InteractAsync(request, context.CancellationToken);
    
        return new()
        {
            Items = { pagedResult.Items.Select(details => (Catalog)details) },
            Page = new()
            {
                Current = pagedResult.Page.Current,
                Size = pagedResult.Page.Size,
                HasNext = pagedResult.Page.HasNext,
                HasPrevious = pagedResult.Page.HasPrevious
            }
        };
    }
    
    public override async Task<CatalogItems> ListCatalogItems(ListCatalogItemsRequest request, ServerCallContext context)
    {
        var pagedResult = await _listCatalogItemsInteractor.InteractAsync(request, context.CancellationToken);
    
        return new()
        {
            Items = { pagedResult.Items.Select(details => (CatalogItem) details) },
            Page = new()
            {
                Current = pagedResult.Page.Current,
                Size = pagedResult.Page.Size,
                HasNext = pagedResult.Page.HasNext,
                HasPrevious = pagedResult.Page.HasPrevious
            }
        };
    }
    
    public override async Task<CatalogItem> GetCatalogItems(GetCatalogItemsRequest request, ServerCallContext context)
        => await _getCatalogItemInteractor.InteractAsync(request, context.CancellationToken);
}