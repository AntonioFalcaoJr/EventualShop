using Application.EventSourcing.Projections;
using ECommerce.Contracts.Payments;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetPaymentDetailsConsumer : IConsumer<Queries.GetPaymentDetails>
{
    private readonly IPaymentProjectionsService _projectionsService;

    public GetPaymentDetailsConsumer(IPaymentProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<Queries.GetPaymentDetails> context)
    {
        var paymentDetails = await _projectionsService.GetPaymentDetailsAsync(context.Message.PaymentId, context.CancellationToken);
        await context.RespondAsync<Responses.PaymentDetails>(paymentDetails);
    }
}