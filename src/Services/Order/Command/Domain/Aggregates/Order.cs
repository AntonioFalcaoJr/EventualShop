using Domain.Abstractions.Aggregates;
using Domain.Entities.OrderItems;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.Order;
using Domain.ValueObjects.Addresses;

namespace Domain.Aggregates;

public class Order : AggregateRoot<OrderValidator>
{
    private readonly List<OrderItem> _items = new();
    private readonly List<PaymentMethod> _paymentMethods = new();

    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Address BillingAddress { get; private set; }
    public Address ShippingAddress { get; private set; }
    public decimal Total { get; private set; }

    public IEnumerable<OrderItem> Items
        => _items;

    public IEnumerable<PaymentMethod> PaymentMethods
        => _paymentMethods;

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.PlaceOrder cmd)
        => RaiseEvent(new DomainEvent.OrderPlaced(
            Guid.NewGuid(),
            cmd.CustomerId,
            cmd.Total,
            cmd.BillingAddress,
            cmd.ShippingAddress,
            cmd.Items.Select(cartItem => (Dto.OrderItem)cartItem),
            cmd.PaymentMethods,
            OrderStatus.PendingPayment));

    private void Handle(Command.ConfirmOrder cmd)
        => RaiseEvent(new DomainEvent.OrderConfirmed(cmd.OrderId, OrderStatus.Confirmed));

    protected override void Apply(IEvent @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.OrderPlaced @event)
    {
        (Id, CustomerId, Total, BillingAddress, ShippingAddress, var items, var paymentMethods, Status) = @event;
        _items.AddRange(items.Select(item => (OrderItem)item));
        _paymentMethods.AddRange(paymentMethods.Select(method => (PaymentMethod)method));
    }

    private void Apply(DomainEvent.OrderConfirmed @event)
        => Status = @event.Status;
}