using Application.Abstractions;
using Contracts.Services.Communication;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class EmailConfirmationRequestedConsumer : Consumer<DomainEvent.EmailConfirmationRequested>
{
    public EmailConfirmationRequestedConsumer(IInteractor<DomainEvent.EmailConfirmationRequested> interactor)
        : base(interactor) { }
}