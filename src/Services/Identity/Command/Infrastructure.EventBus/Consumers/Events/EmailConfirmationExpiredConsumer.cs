using Application.Abstractions;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class EmailConfirmationExpiredConsumer(IInteractor<DelayedEvent.EmailConfirmationExpired> interactor)
    : Consumer<DelayedEvent.EmailConfirmationExpired>(interactor);