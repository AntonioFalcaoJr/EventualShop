using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Shopping.Products;
using Domain.Abstractions.Aggregates;
using Domain.ValueObjects;
using Version = Domain.ValueObjects.Version;

namespace Domain.Aggregates.Products;

public class Product : AggregateRoot<ProductId>
{
    private readonly Dictionary<Currency, Price> _prices = new();

    public ProductName Name { get; private set; } = ProductName.Undefined;
    public PictureUri PictureUri { get; private set; } = PictureUri.Undefined;
    public Sku Sku { get; private set; } = Sku.Undefined;
    public Quantity Stock { get; private set; } = Quantity.Zero;
    public IDictionary<Currency, Price> Prices => _prices.AsReadOnly();

    public static Product Create(ProductId id, ProductName name, Price price)
    {
        Product product = new();
        DomainEvent.ProductCreated @event = new(id, name, price.Amount, price.Currency, Version.Initial);
        product.RaiseEvent(@event);
        return product;
    }

    protected override void ApplyEvent(IDomainEvent @event) => When(@event as dynamic);

    private void When(DomainEvent.ProductCreated @event)
    {
        Id = (ProductId)@event.ProductId;
        Name = (ProductName)@event.Name;

        var currency = (Currency)@event.Currency;
        _prices[currency] = new((Amount)@event.Price, currency);
    }
}