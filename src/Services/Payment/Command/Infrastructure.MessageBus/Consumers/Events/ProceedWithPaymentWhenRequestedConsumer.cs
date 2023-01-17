using Application.UseCases.Events;
using Contracts.Services.Payment;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class ProceedWithPaymentWhenRequestedConsumer : Consumer<DomainEvent.PaymentRequested>
{
    public ProceedWithPaymentWhenRequestedConsumer(IProceedWithPaymentWhenRequestedInteractor interactor)
        : base(interactor) { }
}