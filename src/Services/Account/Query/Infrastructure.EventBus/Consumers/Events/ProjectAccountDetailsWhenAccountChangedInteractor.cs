using Application.UseCases.Events;
using Contracts.Boundaries.Account;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectAccountDetailsWhenAccountChangedInteractor(IProjectAccountDetailsWhenAccountChangedInteractor interactor)
    :
        IConsumer<DomainEvent.AccountCreated>,
        IConsumer<DomainEvent.AccountDeleted>,
        IConsumer<DomainEvent.AccountActivated>,
        IConsumer<DomainEvent.AccountDeactivated>
{
    public Task Consume(ConsumeContext<DomainEvent.AccountCreated> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountDeleted> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountActivated> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountDeactivated> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}