using Application.Abstractions.Projections;
using Domain.Enumerations;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectCartWhenChangedConsumer :
    IConsumer<DomainEvents.BillingAddressChanged>,
    IConsumer<DomainEvents.CartCreated>,
    IConsumer<DomainEvents.CartItemAdded>,
    IConsumer<DomainEvents.CreditCardAdded>,
    IConsumer<DomainEvents.PayPalAdded>,
    IConsumer<DomainEvents.CartItemRemoved>,
    IConsumer<DomainEvents.CartCheckedOut>,
    IConsumer<DomainEvents.ShippingAddressAdded>,
    IConsumer<DomainEvents.CartItemIncreased>,
    IConsumer<DomainEvents.CartItemDecreased>,
    IConsumer<DomainEvents.CartDiscarded>
{
    private readonly IProjectionRepository<ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCart> _projectionRepository;

    public ProjectCartWhenChangedConsumer(IProjectionRepository<ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCart> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.BillingAddressChanged> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Customer.BillingAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartCheckedOut> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Status,
            value: ShoppingCartStatus.CheckedOut.ToString(),
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartCreated> context)
    {
        var customer = new ECommerce.Contracts.ShoppingCarts.Projections.Customer(context.Message.CustomerId);
        var shoppingCart = new ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCart(context.Message.CartId, customer, context.Message.Status);
        await _projectionRepository.InsertAsync(shoppingCart, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvents.CartDiscarded> context)
        => await _projectionRepository.DeleteAsync(context.Message.CartId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartItemAdded> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.Item.Quantity * context.Message.Item.UnitPrice,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartItemDecreased> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice * -1,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartItemIncreased> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartItemRemoved> context)
        => await _projectionRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice * context.Message.Quantity * -1,
            cancellationToken: context.CancellationToken);

    // TODO Segregate Payment Methods projections
    public Task Consume(ConsumeContext<DomainEvents.CreditCardAdded> context)
        => Task.CompletedTask;

    public Task Consume(ConsumeContext<DomainEvents.PayPalAdded> context)
        => Task.CompletedTask;

    public async Task Consume(ConsumeContext<DomainEvents.ShippingAddressAdded> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Customer.ShippingAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);
}