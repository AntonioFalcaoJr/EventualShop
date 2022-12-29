using Application.Abstractions;
using Contracts.Services.Account;
using Infrastructure.EventBus.Abstractions;
using MassTransit;

namespace Infrastructure.EventBus.Consumers;

public class AccountCreatedConsumer : 
    Consumer<DomainEvent.AccountCreated>,
    IConsumer<IntegrationEvent.ProjectionRebuilt>
{
    private readonly IInteractor<IntegrationEvent.ProjectionRebuilt> _projectionRebuiltInteractor;

    public AccountCreatedConsumer(
        IInteractor<DomainEvent.AccountCreated> interactor,
        Func<Type, IInteractor<IntegrationEvent.ProjectionRebuilt>> serviceResolver)
        : base(interactor)
    {
        _projectionRebuiltInteractor = serviceResolver(interactor.GetType().BaseType);
    }

    public Task Consume(ConsumeContext<IntegrationEvent.ProjectionRebuilt> context)
        => _projectionRebuiltInteractor.InteractAsync(context.Message, context.CancellationToken);
}