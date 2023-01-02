using Application.UseCases.Events;
using MassTransit;
using Payment = Contracts.Services.Payment;

namespace Infrastructure.MessageBus.Consumers.Events;

public class ConfirmOrderWhenPaymentCompletedConsumer : IConsumer<Payment.DomainEvent.PaymentCompleted>
{
    private readonly IConfirmOrderWhenPaymentCompletedInteractor _interactor;

    public ConfirmOrderWhenPaymentCompletedConsumer(IConfirmOrderWhenPaymentCompletedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<Payment.DomainEvent.PaymentCompleted> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}