using Contracts.Abstractions.Messages;
using Domain.Abstractions.Aggregates;
using Domain.Aggregates.Catalogs;
using Domain.Aggregates.Products;
using Domain.ValueObjects;
using static Contracts.Boundaries.Cataloging.CatalogItem.DomainEvent;
using static Domain.Exceptions;
using Version = Domain.ValueObjects.Version;

namespace Domain.Aggregates.CatalogItems;

public class CatalogItem : AggregateRoot<CatalogItemId>
{
    public AppId AppId { get; private set; } = AppId.Undefined;
    public CatalogId CatalogId { get; private set; } = CatalogId.Undefined;
    public ProductId ProductId { get; private set; } = ProductId.Undefined;
    public Quantity Quantity { get; private set; } = Quantity.Zero;

    public void Associate(AppId appId, CatalogId catalogId, ProductId productId, Quantity quantity)
    {
        CatalogItemAlreadyAssociated.ThrowIf(catalogId != CatalogId.Undefined);
        RaiseEvent(new CatalogItemAssociated(Id, appId, catalogId, productId, quantity, Version.Initial));
    }
    
    public void Remove() 
        => RaiseEvent<CatalogItemRemoved>(new(CatalogId, Id, Version.Next));

    protected override void ApplyEvent(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(CatalogItemAssociated @event)
    {
        Id = (CatalogItemId)@event.ItemId;
        AppId = (AppId)@event.AppId;
        CatalogId = (CatalogId)@event.CatalogId;
        ProductId = (ProductId)@event.ProductId;
        Quantity = (Quantity)@event.Quantity;
    }

    private void When(CatalogItemRemoved _)
        => IsDeleted = true;
}