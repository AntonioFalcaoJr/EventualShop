using Application.Abstractions;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class EmailChangedConsumer(IInteractor<DomainEvent.EmailChanged> interactor) : Consumer<DomainEvent.EmailChanged>(interactor);