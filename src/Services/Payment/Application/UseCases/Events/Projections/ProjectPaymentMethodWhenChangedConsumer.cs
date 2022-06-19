using Application.Abstractions.Projections;
using Contracts.DataTransferObjects;
using Contracts.Services.Payment;
using Domain.Enumerations;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectPaymentMethodWhenChangedConsumer :
    IConsumer<DomainEvent.PaymentRequested>,
    IConsumer<DomainEvent.PaymentMethodAuthorized>,
    IConsumer<DomainEvent.PaymentMethodDenied>,
    IConsumer<DomainEvent.PaymentMethodCanceled>,
    IConsumer<DomainEvent.PaymentMethodCancellationDenied>,
    IConsumer<DomainEvent.PaymentMethodRefunded>,
    IConsumer<DomainEvent.PaymentMethodRefundDenied>
{
    private readonly IProjectionRepository<Projection.PaymentMethod> _repository;

    public ProjectPaymentMethodWhenChangedConsumer(IProjectionRepository<Projection.PaymentMethod> repository)
        => _repository = repository;

    public Task Consume(ConsumeContext<DomainEvent.PaymentRequested> context)
    {
        var methods = context.Message.PaymentMethods.Select(method
            => new Projection.PaymentMethod(
                method.Id,
                context.Message.PaymentId,
                method.Amount,
                method.Option,
                PaymentMethodStatus.Ready,
                false));

        return _repository.InsertManyAsync(methods, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodAuthorized> context)
        => UpdateStatusAsync(context.Message.PaymentMethodId, PaymentMethodStatus.Authorized, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodDenied> context)
        => UpdateStatusAsync(context.Message.PaymentMethodId, PaymentMethodStatus.Authorized, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodCanceled> context)
        => UpdateStatusAsync(context.Message.PaymentMethodId, PaymentMethodStatus.Authorized, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodCancellationDenied> context)
        => UpdateStatusAsync(context.Message.PaymentMethodId, PaymentMethodStatus.Authorized, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodRefunded> context)
        => UpdateStatusAsync(context.Message.PaymentMethodId, PaymentMethodStatus.Authorized, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PaymentMethodRefundDenied> context)
        => UpdateStatusAsync(context.Message.PaymentMethodId, PaymentMethodStatus.Authorized, context.CancellationToken);

    private Task UpdateStatusAsync(Guid methodId, PaymentMethodStatus status, CancellationToken cancellationToken)
        => _repository.UpdateFieldAsync(methodId, payment => payment.Status, status.Name, cancellationToken);
}