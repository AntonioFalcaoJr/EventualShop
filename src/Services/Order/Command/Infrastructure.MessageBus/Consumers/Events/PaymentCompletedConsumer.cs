using Application.Abstractions;
using Contracts.Services.Payment;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class PaymentCompletedConsumer : Consumer<DomainEvent.PaymentCompleted>
{
    public PaymentCompletedConsumer(IInteractor<DomainEvent.PaymentCompleted> interactor)
        : base(interactor) { }
}