using Application.UseCases.Events;
using Contracts.Services.Identity;
using MassTransit;

namespace Infrastructure.EventBus.Consumers;

public class ProjectUserDetailsWhenUserChangedConsumer : 
    IConsumer<DomainEvent.UserDeleted>,
    IConsumer<DomainEvent.UserRegistered>,
    IConsumer<DomainEvent.UserPasswordChanged>
{
    private readonly IProjectUserDetailsWhenUserChangedInteractor _interactor;
    
    public ProjectUserDetailsWhenUserChangedConsumer(IProjectUserDetailsWhenUserChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.UserDeleted> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.UserRegistered> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.UserPasswordChanged> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}