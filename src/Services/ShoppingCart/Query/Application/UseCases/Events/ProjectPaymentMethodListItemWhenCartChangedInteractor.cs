using Application.Abstractions;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Events;

public interface IProjectPaymentMethodListItemWhenCartChangedInteractor :
    IInteractor<DomainEvent.CreditCardAdded>,
    IInteractor<DomainEvent.DebitCardAdded>,
    IInteractor<DomainEvent.PayPalAdded>,
    IInteractor<DomainEvent.CartDiscarded> { }

public class ProjectPaymentMethodListItemWhenCartChangedInteractor : IProjectPaymentMethodListItemWhenCartChangedInteractor
{
    private readonly IProjectionGateway<Projection.PaymentMethodListItem> _projectionGateway;

    public ProjectPaymentMethodListItemWhenCartChangedInteractor(IProjectionGateway<Projection.PaymentMethodListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CreditCardAdded @event, CancellationToken cancellationToken)
    {
        Projection.PaymentMethodListItem creditCard = new(
            @event.MethodId,
            @event.CartId,
            @event.Amount,
            @event.CreditCard.GetType().Name, // TODO - It's temporary
            false,
            @event.Version);

        await _projectionGateway.ReplaceInsertAsync(creditCard, cancellationToken);
    }

    public async Task InteractAsync(DomainEvent.DebitCardAdded @event, CancellationToken cancellationToken)
    {
        Projection.PaymentMethodListItem creditCard = new(
            @event.MethodId,
            @event.CartId,
            @event.Amount,
            @event.DebitCard.GetType().Name, // TODO - It's temporary
            false,
            @event.Version);

        await _projectionGateway.ReplaceInsertAsync(creditCard, cancellationToken);
    }

    public async Task InteractAsync(DomainEvent.PayPalAdded @event, CancellationToken cancellationToken)
    {
        Projection.PaymentMethodListItem creditCard = new(
            @event.MethodId,
            @event.CartId,
            @event.Amount,
            @event.PayPal.GetType().Name, // TODO - It's temporary
            false,
            @event.Version);

        await _projectionGateway.ReplaceInsertAsync(creditCard, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.CartDiscarded @event, CancellationToken cancellationToken)
        => _projectionGateway.DeleteAsync(item => item.CartId == @event.CartId, cancellationToken);
}