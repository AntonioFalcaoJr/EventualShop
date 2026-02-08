using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Warehouse.Product;
using Domain.Abstractions.Aggregates;
using Domain.ValueObjects;
using Version = Domain.ValueObjects.Version;

namespace Domain.Aggregates.Products;

public class Product : AggregateRoot<ProductId>
{
    public ProductName Name { get; private set; } = ProductName.Undefined;
    public Description Description { get; private set; } = Description.Undefined;
    public Weight Weight { get; private set; } = Weight.Zero;
    public Dimensions Dimensions { get; private set; } = Dimensions.Zero;
    public Brand Brand { get; private set; } = Brand.Undefined;
    public Category Category { get; private set; } = Category.Undefined;
    public Unit Unit { get; private set; } = Unit.Unspecified;
    public PictureUri PictureUri { get; private set; } = PictureUri.Undefined;
    public Sku Sku { get; private set; } = Sku.Undefined;
    public Money Cost { get; private set; } = Money.Zero(Currency.Undefined);

    public static Product Register(ProductId productId, ProductName name, Description description, Weight weight,
        Dimensions dimensions, Brand brand, Category category, Unit unit, PictureUri pictureUri, Sku sku, Money cost)
    {
        Product product = new();

        DomainEvent.ProductRegistered @event = new(productId, name, description, weight, dimensions.Length, dimensions.Width,
            dimensions.Height, brand, category, unit, pictureUri, sku, cost.Currency, cost.Amount, Version.Initial);

        product.RaiseEvent(@event);
        return product;
    }

    protected override void ApplyEvent(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.ProductRegistered @event)
    {
        Id = (ProductId)@event.ProductId;
        Name = (ProductName)@event.ProductName;
        Description = (Description)@event.Description;
        Weight = (Weight)@event.Weight;
        Dimensions = ((Length)@event.Length, (Width)@event.Width, (Height)@event.Height);
        Brand = (Brand)(@event.Brand, @event.Brand);
        Category = (Category)@event.Category;
        Unit = (Unit)@event.Unit;
        PictureUri = (PictureUri)@event.PictureUri;
        Sku = (Sku)@event.Sku;
        Cost = ((Amount)@event.Amount, (Currency)@event.Currency);
    }
}