using Application.UseCases.Events;
using Contracts.Boundaries.Payment;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectPaymentMethodDetailsWhenChangedConsumer(IProjectPaymentMethodDetailsWhenChangedInteractor interactor)
    :
        IConsumer<DomainEvent.PaymentMethodAuthorized>,
        IConsumer<DomainEvent.PaymentMethodDenied>,
        IConsumer<DomainEvent.PaymentMethodCanceled>,
        IConsumer<DomainEvent.PaymentMethodCancellationDenied>,
        IConsumer<DomainEvent.PaymentMethodRefunded>,
        IConsumer<DomainEvent.PaymentMethodRefundDenied> 
{
    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodAuthorized> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodDenied> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodCanceled> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodCancellationDenied> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodRefunded> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodRefundDenied> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}