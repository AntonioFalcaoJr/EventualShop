using Application.UseCases.Events;
using Contracts.Boundaries.Account;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectShippingAddressListItemWhenAccountChangedConsumer(IProjectShippingAddressListItemWhenAccountChangedInteractor interactor)
    :
        IConsumer<DomainEvent.ShippingAddressAdded>,
        IConsumer<DomainEvent.AccountDeleted>
{
    public Task Consume(ConsumeContext<DomainEvent.ShippingAddressAdded> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountDeleted> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}