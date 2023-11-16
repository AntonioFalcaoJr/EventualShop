using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Cataloging.CatalogItem;
using Domain.Abstractions.Aggregates;
using Domain.Aggregates.Catalogs;
using Domain.Aggregates.Products;
using Version = Domain.ValueObjects.Version;

namespace Domain.Aggregates.CatalogItems;

public class CatalogItem : AggregateRoot<CatalogItemId>
{
    public AppId AppId { get; private set; } = AppId.Undefined;
    public CatalogId CatalogId { get; private set; } = CatalogId.Undefined;
    public ProductId ProductId { get; private set; } = ProductId.Undefined;

    public static CatalogItem Create(AppId appId, CatalogId catalogId, ProductId productId)
    {
        CatalogItem item = new();
        DomainEvent.CatalogItemCreated @event = new(item.Id, appId, catalogId, productId, Version.Initial);
        item.RaiseEvent(@event);
        return item;
    }

    public void RemoveCatalogItem(CatalogItemId itemId)
    {
        // TODO: specify the exception type
        if (IsDeleted)
            throw new InvalidOperationException("Catalog item is already deleted.");

        RaiseEvent(new DomainEvent.CatalogItemRemoved(Id, itemId, Version.Next));
    }

    protected override void ApplyEvent(IDomainEvent @event) => When(@event as dynamic);

    private void When(DomainEvent.CatalogItemCreated @event)
    {
        Id = (CatalogItemId)@event.ItemId;
        AppId = (AppId)@event.AppId;
        CatalogId = (CatalogId)@event.CatalogId;
        ProductId = (ProductId)@event.ProductId;
    }

    private void When(DomainEvent.CatalogItemRemoved _)
        => IsDeleted = true;
}