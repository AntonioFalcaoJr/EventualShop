using Application.UseCases.Events;
using Contracts.Boundaries.Account;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectBillingAddressListItemWhenAccountChangedConsumer(IProjectBillingAddressListItemWhenAccountChangedInteractor interactor)
    :
        IConsumer<DomainEvent.BillingAddressAdded>,
        IConsumer<DomainEvent.AccountDeleted>
{
    public Task Consume(ConsumeContext<DomainEvent.BillingAddressAdded> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountDeleted> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}