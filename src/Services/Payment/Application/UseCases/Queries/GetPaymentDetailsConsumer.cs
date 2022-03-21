using Application.EventSourcing.Projections;
using ECommerce.Contracts.Payment;
using MassTransit;
using GetPaymentDetailsQuery =  ECommerce.Contracts.Payment.Queries.GetPaymentDetails;

namespace Application.UseCases.Queries;

public class GetPaymentDetailsConsumer : IConsumer<GetPaymentDetailsQuery>
{
    private readonly IPaymentProjectionsService _projectionsService;

    public GetPaymentDetailsConsumer(IPaymentProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetPaymentDetailsQuery> context)
    {
        var paymentDetails = await _projectionsService.GetPaymentDetailsAsync(context.Message.PaymentId, context.CancellationToken);
        await context.RespondAsync<Responses.PaymentDetails>(paymentDetails);
    }
}