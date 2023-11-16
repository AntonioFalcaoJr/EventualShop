using Application.Abstractions;
using Contracts.Boundaries.Payment;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class CancelPaymentConsumer(IInteractor<Command.CancelPayment> interactor) : Consumer<Command.CancelPayment>(interactor);