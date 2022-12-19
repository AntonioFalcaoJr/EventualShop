using Application.Abstractions;
using Contracts.Services.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class AccountRebuiltConsumer : Consumer<IntegrationEvent.ProjectionRebuilt>
{
    public AccountRebuiltConsumer(IInteractor<IntegrationEvent.ProjectionRebuilt> interactor) 
        : base(interactor) { }
}
