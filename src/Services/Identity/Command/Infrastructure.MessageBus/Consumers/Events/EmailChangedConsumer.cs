using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class EmailChangedConsumer : Consumer<DomainEvent.EmailChanged>
{
    public EmailChangedConsumer(IInteractor<DomainEvent.EmailChanged> interactor)
        : base(interactor) { }
}