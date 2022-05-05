using Domain.Abstractions.Aggregates;
using Domain.Entities.Customers;
using Domain.Entities.OrderItems;
using Domain.Entities.PaymentMethods;
using Domain.Enumerations;
using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.Order;
using Domain.ValueObjects.PaymentOptions.CreditCards;
using Domain.ValueObjects.PaymentOptions.DebitCards;
using Domain.ValueObjects.PaymentOptions.PayPals;

namespace Domain.Aggregates;

public class Order : AggregateRoot<Guid, OrderValidator>
{
    private readonly List<OrderItem> _items = new();
    private readonly List<PaymentMethod> _paymentMethods = new();
    public OrderStatus Status { get; private set; }
    public Customer Customer { get; private set; }

    public decimal Total
        => Items.Sum(item
            => item.Product.UnitPrice * item.Quantity);

    public IEnumerable<PaymentMethod> PaymentMethods
        => _paymentMethods;

    public IEnumerable<OrderItem> Items
        => _items;

    public void Handle(Command.PlaceOrder cmd)
        => RaiseEvent(new DomainEvent.OrderPlaced(Guid.NewGuid(), cmd.Customer, cmd.Items, cmd.Total, cmd.PaymentMethods));

    public void Handle(Command.ConfirmOrder cmd)
        => RaiseEvent(new DomainEvent.OrderConfirmed(cmd.OrderId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.OrderPlaced @event)
    {
        Id = @event.OrderId;
        Customer = @event.Customer;
        _items.AddRange(@event.Items.Select(item => (OrderItem) item));
        _paymentMethods.AddRange(@event.PaymentMethods.Select<Dto.PaymentMethod, PaymentMethod>(method
            => method.Option switch
            {
                Dto.CreditCard creditCard => new(method.Id, method.Amount, (CreditCard) creditCard),
                Dto.DebitCard debitCard => new(method.Id, method.Amount, (DebitCard) debitCard),
                Dto.PayPal payPal => new(method.Id, method.Amount, (PayPal) payPal),
                _ => default
            }));
    }

    private void When(DomainEvent.OrderConfirmed _)
        => Status = OrderStatus.Confirmed;
}