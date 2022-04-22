using Application.Abstractions.EventSourcing.Projections;
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
    private readonly IProjectionsRepository<ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCart> _projectionsRepository;

    public ProjectCartWhenChangedConsumer(IProjectionsRepository<ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCart> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public Task Consume(ConsumeContext<DomainEvents.BillingAddressChanged> context)
        => _projectionsRepository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Customer.BillingAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartCheckedOut> context)
        => _projectionsRepository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Status,
            value: ShoppingCartStatus.CheckedOut.ToString(),
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CartCreated> context)
    {
        var customer = new ECommerce.Contracts.ShoppingCarts.Projections.Customer(context.Message.CustomerId);
        var shoppingCart = new ECommerce.Contracts.ShoppingCarts.Projections.ShoppingCart(context.Message.CartId, customer, context.Message.Status);
        await _projectionsRepository.InsertAsync(shoppingCart, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvents.CartDiscarded> context)
        => _projectionsRepository.DeleteAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemAdded> context)
        => _projectionsRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.Quantity * context.Message.UnitPrice,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemDecreased> context)
        => _projectionsRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice * -1,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemIncreased> context)
        => _projectionsRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemRemoved> context)
        => _projectionsRepository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice * context.Message.Quantity * -1,
            cancellationToken: context.CancellationToken);

    // TODO Segregate Payment Methods projections
    public Task Consume(ConsumeContext<DomainEvents.CreditCardAdded> context)
        => Task.CompletedTask;

    public Task Consume(ConsumeContext<DomainEvents.PayPalAdded> context)
        => Task.CompletedTask;

    public Task Consume(ConsumeContext<DomainEvents.ShippingAddressAdded> context)
        => _projectionsRepository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Customer.ShippingAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);
}