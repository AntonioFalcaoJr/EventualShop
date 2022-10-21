using Application.Abstractions.Projections;
using Contracts.Services.ShoppingCart;
using Domain.Enumerations;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectCartWhenChangedConsumer :
    IConsumer<DomainEvent.BillingAddressAdded>,
    IConsumer<DomainEvent.CartCreated>,
    IConsumer<DomainEvent.CartItemAdded>,
    IConsumer<DomainEvent.CartItemRemoved>,
    IConsumer<DomainEvent.CartCheckedOut>,
    IConsumer<DomainEvent.ShippingAddressAdded>,
    IConsumer<DomainEvent.CartItemIncreased>,
    IConsumer<DomainEvent.CartItemDecreased>,
    IConsumer<DomainEvent.CartDiscarded>,
    IConsumer<IntegrationEvent.ProjectionRebuilt>
{
    private readonly IProjectionRepository<Projection.ShoppingCart> _repository;

    public ProjectCartWhenChangedConsumer(IProjectionRepository<Projection.ShoppingCart> repository)
    {
        _repository = repository;
    }
    
    public Task Consume(ConsumeContext<IntegrationEvent.ProjectionRebuilt> context)
    {
        Projection.ShoppingCart shoppingCart = new(
            context.Message.Cart.Id,
            context.Message.Cart.CustomerId,
            context.Message.Cart.BillingAddress,
            context.Message.Cart.ShippingAddress,
            context.Message.Cart.Status,
            context.Message.Cart.Total,
            false);

        return _repository.ReplaceAsync(shoppingCart, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvent.BillingAddressAdded> context)
        => _repository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.BillingAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.ShippingAddressAdded> context)
        => _repository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.ShippingAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartCheckedOut> context)
        => _repository.UpdateFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Status,
            value: CartStatus.CheckedOut.Name,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartCreated> context)
    {
        Projection.ShoppingCart shoppingCart = new(
            context.Message.CartId,
            context.Message.CustomerId,
            default,
            default,
            context.Message.Status,
            default,
            false);

        return _repository.InsertAsync(shoppingCart, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => _repository.DeleteAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
        => _repository.IncreaseFieldAsync(
            id: context.Message.Id,
            field: cart => cart.Total,
            value: context.Message.Quantity * context.Message.UnitPrice,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemDecreased> context)
        => _repository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice * -1,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemIncreased> context)
        => _repository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemRemoved> context)
        => _repository.IncreaseFieldAsync(
            id: context.Message.CartId,
            field: cart => cart.Total,
            value: context.Message.UnitPrice * context.Message.Quantity * -1,
            cancellationToken: context.CancellationToken);
}