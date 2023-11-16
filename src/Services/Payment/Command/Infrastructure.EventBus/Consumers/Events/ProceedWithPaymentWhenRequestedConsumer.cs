using Application.UseCases.Events;
using Contracts.Boundaries.Payment;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProceedWithPaymentWhenRequestedConsumer(IProceedWithPaymentWhenRequestedInteractor interactor) : Consumer<DomainEvent.PaymentRequested>(interactor);