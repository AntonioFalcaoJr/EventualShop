using Application.Abstractions.Projections;
using Domain.Enumerations;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectCartWhenChangedConsumer :
    IConsumer<DomainEvent.BillingAddressChanged>,
    IConsumer<DomainEvent.CartCreated>,
    IConsumer<DomainEvent.CartItemAdded>,
    IConsumer<DomainEvent.CreditCardAdded>,
    IConsumer<DomainEvent.PayPalAdded>,
    IConsumer<DomainEvent.CartItemRemoved>,
    IConsumer<DomainEvent.CartCheckedOut>,
    IConsumer<DomainEvent.ShippingAddressAdded>,
    IConsumer<DomainEvent.CartItemIncreased>,
    IConsumer<DomainEvent.CartItemDecreased>,
    IConsumer<DomainEvent.CartDiscarded>
{
    private readonly IProjectionRepository<Projection.ShoppingCart> _projectionRepository;

    public ProjectCartWhenChangedConsumer(IProjectionRepository<Projection.ShoppingCart> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.BillingAddressChanged> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Customer.BillingAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CartCheckedOut> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Status,
            value: ShoppingCartStatus.CheckedOut.ToString(),
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CartCreated> context)
    {
        var customer = new Projection.Customer(context.Message.CustomerId);
        var shoppingCart = new Projection.ShoppingCart(context.Message.CartId, customer, context.Message.Status);
        await _projectionRepository.InsertAsync(shoppingCart, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => await _projectionRepository.DeleteAsync(context.Message.CartId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.Item.Quantity * context.Message.Item.UnitPrice,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CartItemDecreased> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice * -1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CartItemIncreased> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CartItemRemoved> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice * context.Message.Quantity * -1,
            cancellationToken: context.CancellationToken);

    // TODO Segregate Payment Methods projections
    public Task Consume(ConsumeContext<DomainEvent.CreditCardAdded> context)
        => Task.CompletedTask;

    public Task Consume(ConsumeContext<DomainEvent.PayPalAdded> context)
        => Task.CompletedTask;

    public async Task Consume(ConsumeContext<DomainEvent.ShippingAddressAdded> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Customer.ShippingAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);
}