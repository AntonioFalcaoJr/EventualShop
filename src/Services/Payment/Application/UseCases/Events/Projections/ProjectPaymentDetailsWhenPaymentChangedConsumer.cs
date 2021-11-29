using System;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Services.Payments;

namespace Application.UseCases.Events.Projections;

public class ProjectPaymentDetailsWhenPaymentChangedConsumer :
    IConsumer<DomainEvents.PaymentCanceled>,
    IConsumer<DomainEvents.PaymentRequested>
{
    private readonly IPaymentEventStoreService _eventStoreService;
    private readonly IPaymentProjectionsService _projectionsService;

    public ProjectPaymentDetailsWhenPaymentChangedConsumer(IPaymentEventStoreService eventStoreService, IPaymentProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<DomainEvents.PaymentCanceled> context)
        => ProjectAsync(context.Message.PaymentId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.PaymentRequested> context)
        => ProjectAsync(context.Message.PaymentId, context.CancellationToken);

    private async Task ProjectAsync(Guid paymentId, CancellationToken cancellationToken)
    {
        var payment = await _eventStoreService.LoadAggregateFromStreamAsync(paymentId, cancellationToken);

        var paymentDetails = new PaymentDetailsProjection
        {
            Id = payment.Id,
            IsDeleted = payment.IsDeleted,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Status = payment.Status.ToString(),
            BillingAddressProjection = payment.BillingAddress is null
                ? default
                : new()
                {
                    City = payment.BillingAddress.City,
                    Country = payment.BillingAddress.Country,
                    Number = payment.BillingAddress.Number,
                    State = payment.BillingAddress.State,
                    Street = payment.BillingAddress.Street,
                    ZipCode = payment.BillingAddress.ZipCode
                },
            // CreditCardProjection = payment.CreditCardPaymentMethod is null
            //     ? default
            //     : new()
            //     {
            //         Expiration = payment.CreditCardPaymentMethod.Expiration,
            //         Number = payment.CreditCardPaymentMethod.Number,
            //         HolderName = payment.CreditCardPaymentMethod.HolderName,
            //         SecurityNumber = payment.CreditCardPaymentMethod.SecurityNumber
            //     }
        };

        await _projectionsService.UpdatePaymentDetailsAsync(paymentDetails, cancellationToken);
    }
}