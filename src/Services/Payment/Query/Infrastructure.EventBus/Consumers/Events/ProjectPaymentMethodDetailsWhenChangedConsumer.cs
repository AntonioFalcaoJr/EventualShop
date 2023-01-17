using Application.UseCases.Events;
using Contracts.Services.Payment;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectPaymentMethodDetailsWhenChangedConsumer :
    IConsumer<DomainEvent.PaymentRequested>,
    IConsumer<DomainEvent.PaymentMethodAuthorized>,
    IConsumer<DomainEvent.PaymentMethodDenied>,
    IConsumer<DomainEvent.PaymentMethodCanceled>,
    IConsumer<DomainEvent.PaymentMethodCancellationDenied>,
    IConsumer<DomainEvent.PaymentMethodRefunded>,
    IConsumer<DomainEvent.PaymentMethodRefundDenied> 
{
    private readonly IProjectPaymentMethodDetailsWhenChangedInteractor _interactor;

    public ProjectPaymentMethodDetailsWhenChangedConsumer(IProjectPaymentMethodDetailsWhenChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.PaymentRequested> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodAuthorized> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodDenied> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodCanceled> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodCancellationDenied> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodRefunded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodRefundDenied> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}