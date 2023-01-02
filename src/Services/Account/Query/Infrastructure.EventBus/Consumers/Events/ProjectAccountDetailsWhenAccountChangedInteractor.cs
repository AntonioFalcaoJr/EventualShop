using Application.UseCases.Events;
using Contracts.Services.Account;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectAccountDetailsWhenAccountChangedInteractor :
    IConsumer<DomainEvent.AccountCreated>,
    IConsumer<DomainEvent.AccountDeleted>,
    IConsumer<DomainEvent.AccountActivated>,
    IConsumer<DomainEvent.AccountDeactivated>
{
    private readonly IProjectAccountDetailsWhenAccountChangedInteractor _interactor;

    public ProjectAccountDetailsWhenAccountChangedInteractor(IProjectAccountDetailsWhenAccountChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.AccountCreated> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountDeleted> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountActivated> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountDeactivated> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}