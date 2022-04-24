using Application.Abstractions.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Payments;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetPaymentDetailsConsumer : IConsumer<Queries.GetPaymentDetails>
{
    private readonly IProjectionRepository<Projection.Payment> _repository;

    public GetPaymentDetailsConsumer(IProjectionRepository<Projection.Payment> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Queries.GetPaymentDetails> context)
    {
        var paymentD = await _repository.GetAsync(context.Message.PaymentId, context.CancellationToken);

        await (paymentD is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(paymentD));
    }
}