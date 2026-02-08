using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Shopping.Product;
using Domain.Abstractions.Aggregates;
using Domain.Aggregates.ShoppingCarts;
using Domain.Extensions;
using Domain.ValueObjects;
using static Domain.Exceptions;
using Version = Domain.ValueObjects.Version;

namespace Domain.Aggregates.Products;

public class Product : AggregateRoot<ProductId>
{
    private Dictionary<Currency, Price> _prices = new();
    private Dictionary<CartId, Reservation> _reservations = new();

    public ProductName Name { get; private set; } = ProductName.Undefined;
    public PictureUri PictureUri { get; private set; } = PictureUri.Undefined;
    public Sku Sku { get; private set; } = Sku.Undefined;
    public Quantity Availability { get; private set; } = Quantity.Zero;
    public Quantity Reserved { get; private set; } = Quantity.Zero;
    public Quantity Stock => Availability - Reserved;
    public IDictionary<Currency, Price> Prices => _prices.AsReadOnly();

    public static Product Release(CatalogItemId catalogItemId, ProductName name, IDictionary<Currency, Price> prices, Quantity quantity)
    {
        Product product = new();
        DomainEvent.ProductReleased @event = new(product.Id, catalogItemId, name, prices.AsString(), quantity, Version.Initial);
        product.RaiseEvent(@event);
        return product;
    }

    public void ReserveStock(CartId cartId, Quantity quantity)
    {
        InsufficientStockException.ThrowIf(quantity > Availability);

        var newReservation = _reservations[cartId] + quantity;
        var newTotalReserved = Reserved + quantity;

        RaiseEvent(new DomainEvent.ProductReserved(Id, cartId, quantity, newReservation, newTotalReserved, Version.Next));
    }

    public void ChangeStockReservation(CartId cartId, Quantity requestedQuantity)
    {
        InsufficientStockException.ThrowIf(requestedQuantity > Stock);

        _reservations.TryGetValue(cartId, out var reservation);

        ReservationNotFound.ThrowIf(reservation is null);

        if (requestedQuantity == reservation!.Quantity) return;

        RaiseEvent(requestedQuantity switch
        {
            _ when requestedQuantity == Quantity.Zero => ReservationReleased(),
            _ when requestedQuantity < reservation.Quantity => ReservationDecreased(),
            _ when requestedQuantity > reservation.Quantity => ReservationIncreased(),
            _ => InvalidQuantity.Throw()
        });

        DomainEvent.ReservationReleased ReservationReleased() =>
            new(Id, cartId, requestedQuantity, Reserved - reservation.Quantity, Version.Next);

        DomainEvent.ReservationDecreased ReservationDecreased() =>
            new(Id, cartId, requestedQuantity, Reserved - requestedQuantity, Version.Next);

        DomainEvent.ReservationIncreased ReservationIncreased() =>
            new(Id, cartId, requestedQuantity, Reserved + requestedQuantity, Version.Next);
    }

    public void TakeStock(Quantity quantity)
    {
        InsufficientStockException.ThrowIf(quantity > Availability);

        var newInventory = Availability - quantity;

        RaiseEvent(new DomainEvent.ProductTaken(Id, quantity, newInventory, Version.Next));
    }
    
    public void Restock(Quantity quantity)
    {
        var newAvailability = Availability + quantity;
        RaiseEvent(new DomainEvent.ProductRestocked(Id, quantity, newAvailability, Version.Next));
    }

    protected override void ApplyEvent(IDomainEvent @event) => When(@event as dynamic);

    private void When(DomainEvent.ProductReleased @event)
    {
        Id = (ProductId)@event.ProductId;
        Name = (ProductName)@event.Name;
        Availability = (Quantity)@event.Availability;
        _prices = @event.Prices.ToPriceDictionary();
    }

    private void When(DomainEvent.ProductTaken @event)
    {
        Availability = (Quantity)@event.NewInventory;
    }

    private void When(DomainEvent.ProductReserved @event)
    {
        Reservation reservation = new((CartId)@event.CartId, (Quantity)@event.Quantity);
        _reservations.Add(reservation.CartId, reservation);
        Reserved = (Quantity)@event.NewTotalReserved;
    }
}

public record Reservation(CartId CartId, Quantity Quantity)
{
    public bool HasExpired => DateTimeOffset.UtcNow > ExpirationTime;
    public DateTimeOffset ExpirationTime { get; } = DateTimeOffset.UtcNow.AddMinutes(10);

    public static Reservation Empty(CartId cartId) => new(cartId, Quantity.Zero);

    public Reservation AddQuantity(Quantity additionalQuantity) =>
        this with { Quantity = Quantity + additionalQuantity };
}