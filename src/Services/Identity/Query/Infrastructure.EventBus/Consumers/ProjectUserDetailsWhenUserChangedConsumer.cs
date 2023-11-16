using Application.UseCases.Events;
using Contracts.Boundaries.Identity;
using MassTransit;

namespace Infrastructure.EventBus.Consumers;

public class ProjectUserDetailsWhenUserChangedConsumer(IProjectUserDetailsWhenUserChangedInteractor interactor) :
    IConsumer<DomainEvent.UserDeleted>,
    IConsumer<DomainEvent.UserRegistered>,
    IConsumer<DomainEvent.UserPasswordChanged>
{
    public Task Consume(ConsumeContext<DomainEvent.UserDeleted> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.UserRegistered> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.UserPasswordChanged> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}