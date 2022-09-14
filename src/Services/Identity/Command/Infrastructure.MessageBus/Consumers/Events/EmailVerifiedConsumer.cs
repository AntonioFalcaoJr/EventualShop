using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class EmailVerifiedConsumer : Consumer<DomainEvent.EmailVerified>
{
    public EmailVerifiedConsumer(IInteractor<DomainEvent.EmailVerified> interactor)
        : base(interactor) { }
}