using Application.Abstractions;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Events;

public interface IProjectPaymentMethodListItemWhenCartChangedInteractor :
    IInteractor<DomainEvent.PaymentMethodAdded>,
    IInteractor<DomainEvent.CartDiscarded> { }

public class ProjectPaymentMethodListItemWhenCartChangedInteractor : IProjectPaymentMethodListItemWhenCartChangedInteractor
{
    private readonly IProjectionGateway<Projection.PaymentMethodListItem> _projectionGateway;

    public ProjectPaymentMethodListItemWhenCartChangedInteractor(IProjectionGateway<Projection.PaymentMethodListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task InteractAsync(DomainEvent.PaymentMethodAdded @event, CancellationToken cancellationToken)
    {
        Projection.PaymentMethodListItem creditCard = new(
            @event.MethodId,
            @event.CartId,
            @event.Amount,
            @event.Option?.ToString()!, // TODO - It's temporary
            false);

        return _projectionGateway.UpsertAsync(creditCard, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.CartDiscarded @event, CancellationToken cancellationToken)
        => _projectionGateway.DeleteAsync(item => item.CartId == @event.CartId, cancellationToken);
}