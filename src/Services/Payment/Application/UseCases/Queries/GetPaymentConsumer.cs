using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Payment;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetPaymentConsumer : IConsumer<Query.GetPayment>
{
    private readonly IProjectionRepository<Projection.Payment> _repository;

    public GetPaymentConsumer(IProjectionRepository<Projection.Payment> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetPayment> context)
    {
        var payment = await _repository.GetAsync(context.Message.PaymentId, context.CancellationToken);

        await (payment is null
            ? context.RespondAsync<Reply.NotFound>(new())
            : context.RespondAsync(payment));
    }
}