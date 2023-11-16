using Application.UseCases.Events;
using Contracts.Boundaries.Payment;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectPaymentDetailsWhenChangedConsumer(IProjectPaymentWhenChangedInteractor interactor) :
    IConsumer<DomainEvent.PaymentRequested>,
    IConsumer<DomainEvent.PaymentCanceled>
{
    public Task Consume(ConsumeContext<DomainEvent.PaymentRequested> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentCanceled> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}