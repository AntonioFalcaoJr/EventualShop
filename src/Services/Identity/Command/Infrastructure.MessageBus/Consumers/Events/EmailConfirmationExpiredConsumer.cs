using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class EmailConfirmationExpiredConsumer : Consumer<DelayedEvent.EmailConfirmationExpired>
{
    public EmailConfirmationExpiredConsumer(IInteractor<DelayedEvent.EmailConfirmationExpired> interactor)
        : base(interactor) { }
}