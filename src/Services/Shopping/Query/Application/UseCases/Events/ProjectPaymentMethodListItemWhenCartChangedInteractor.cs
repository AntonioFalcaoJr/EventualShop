using Application.Abstractions;
using Contracts.Boundaries.Shopping.ShoppingCart;

namespace Application.UseCases.Events;

public interface IProjectPaymentMethodListItemWhenCartChangedInteractor :
    // IInteractor<DomainEvent.CreditCardAdded>,
    // IInteractor<DomainEvent.DebitCardAdded>,
    // IInteractor<DomainEvent.PayPalAdded>,
    IInteractor<DomainEvent.CartDiscarded>;

public class ProjectPaymentMethodListItemWhenCartChangedInteractor(IProjectionGateway<Projection.PaymentMethodListItem> projectionGateway)
    : IProjectPaymentMethodListItemWhenCartChangedInteractor
{
    // public async Task InteractAsync(DomainEvent.CreditCardAdded @event, CancellationToken cancellationToken)
    // {
    //     Projection.PaymentMethodListItem creditCard = new(
    //         @event.MethodId,
    //         @event.ShoppingCartId,
    //         @event.Amount,
    //         @event.CreditCard.GetType().Name, // TODO - It's temporary
    //         false,
    //         @event.Version);
    //
    //     await projectionGateway.ReplaceInsertAsync(creditCard, cancellationToken);
    // }
    //
    // public async Task InteractAsync(DomainEvent.DebitCardAdded @event, CancellationToken cancellationToken)
    // {
    //     Projection.PaymentMethodListItem creditCard = new(
    //         @event.MethodId,
    //         @event.ShoppingCartId,
    //         @event.Amount,
    //         @event.DebitCard.GetType().Name, // TODO - It's temporary
    //         false,
    //         @event.Version);
    //
    //     await projectionGateway.ReplaceInsertAsync(creditCard, cancellationToken);
    // }
    //
    // public async Task InteractAsync(DomainEvent.PayPalAdded @event, CancellationToken cancellationToken)
    // {
    //     Projection.PaymentMethodListItem creditCard = new(
    //         @event.MethodId,
    //         @event.ShoppingCartId,
    //         @event.Amount,
    //         @event.PayPal.GetType().Name, // TODO - It's temporary
    //         false,
    //         @event.Version);
    //
    //     await projectionGateway.ReplaceInsertAsync(creditCard, cancellationToken);
    // }

    public Task InteractAsync(DomainEvent.CartDiscarded @event, CancellationToken cancellationToken)
        => projectionGateway.DeleteAsync(item => item.CartId == Guid.Parse(@event.CartId), cancellationToken);
}