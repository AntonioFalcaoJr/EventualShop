using Application.Abstractions;
using Contracts.Services.Payment;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class CancelPaymentConsumer : Consumer<Command.CancelPayment>
{
    public CancelPaymentConsumer(IInteractor<Command.CancelPayment> interactor)
        : base(interactor) { }
}