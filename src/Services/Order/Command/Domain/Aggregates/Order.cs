using Domain.Abstractions.Aggregates;
using Domain.Entities.OrderItems;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.Order;
using Domain.ValueObjects;
using Domain.ValueObjects.Addresses;
using Newtonsoft.Json;

namespace Domain.Aggregates;

public class Order : AggregateRoot<OrderValidator>
{
    [JsonProperty]
    private readonly List<OrderItem> _items = new();

    [JsonProperty]
    private readonly List<PaymentMethod> _paymentMethods = new();

    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Empty;
    public Address BillingAddress { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Money Total { get; private set; } = Money.Zero(Currency.Undefined);

    public IEnumerable<OrderItem> Items
        => _items.AsReadOnly();

    public IEnumerable<PaymentMethod> PaymentMethods
        => _paymentMethods.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.PlaceOrder cmd)
        => RaiseEvent<DomainEvent.OrderPlaced>(version
            => new(
                Guid.NewGuid(),
                cmd.CustomerId,
                cmd.Total,
                cmd.BillingAddress,
                cmd.ShippingAddress,
                cmd.Items.Select(cartItem => (Dto.OrderItem)cartItem),
                cmd.PaymentMethods,
                OrderStatus.PendingPayment, version));

    private void Handle(Command.ConfirmOrder cmd)
        => RaiseEvent<DomainEvent.OrderConfirmed>(version => new(cmd.OrderId, OrderStatus.Confirmed, version));

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.OrderPlaced @event)
    {
        (Id, CustomerId, Total, BillingAddress, ShippingAddress, var items, var paymentMethods, Status, _) = @event;
        _items.AddRange(items.Select(item => (OrderItem)item));
        _paymentMethods.AddRange(paymentMethods.Select(method => (PaymentMethod)method));
    }

    private void When(DomainEvent.OrderConfirmed @event)
        => Status = @event.Status;
}