using Application.Services;
using Domain.Aggregates;
using Domain.Aggregates.CatalogItems;
using Domain.Aggregates.Catalogs;
using Domain.Aggregates.Products;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.CatalogItems.Commands;

public record AssociateCatalogItem(AppId AppId, CatalogId CatalogId, InventoryItemId InventoryItemId, Quantity Quantity) : IRequest;

public class CreateCatalogItemInteractor(IApplicationService service) : IRequestHandler<AssociateCatalogItem>
{
    public async Task Handle(AssociateCatalogItem cmd, CancellationToken token)
    {
        var product = await service.LoadAggregateByReferenceIdAsync<Product, ProductId>(cmd.InventoryItemId, token);

        product.TakeInventory(cmd.Quantity);

        CatalogItem item = new();
        item.Associate(cmd.AppId, cmd.CatalogId, cmd.ProductId, cmd.Quantity);

        await service.AppendEventsAsync<Product, ProductId>(product, token);
        await service.AppendEventsAsync<CatalogItem, CatalogItemId>(item, token);
    }
}