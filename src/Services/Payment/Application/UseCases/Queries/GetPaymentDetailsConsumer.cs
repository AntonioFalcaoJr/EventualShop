using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Payment;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetPaymentDetailsConsumer : IConsumer<Query.GetPaymentDetails>
{
    private readonly IProjectionRepository<Projection.Payment> _repository;

    public GetPaymentDetailsConsumer(IProjectionRepository<Projection.Payment> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetPaymentDetails> context)
    {
        var paymentD = await _repository.GetAsync(context.Message.PaymentId, context.CancellationToken);

        await (paymentD is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(paymentD));
    }
}