using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetCartPaymentMethodConsumer :
    IConsumer<Query.GetCartPaymentMethod>,
    IConsumer<Query.GetCartPaymentMethods>
{
    private readonly IProjectionRepository<Projection.PaymentMethod> _repository;

    public GetCartPaymentMethodConsumer(IProjectionRepository<Projection.PaymentMethod> repository)
        => _repository = repository;

    public async Task Consume(ConsumeContext<Query.GetCartPaymentMethod> context)
    {
        var paymentMethod = await _repository.FindAsync(
            predicate: paymentMethod => paymentMethod.CartId == context.Message.CartId &&
                                        paymentMethod.Id == context.Message.PaymentMethodId,
            cancellationToken: context.CancellationToken);

        await (paymentMethod is null
            ? context.RespondAsync<Reply.NotFound>(new())
            : context.RespondAsync(paymentMethod));
    }

    public async Task Consume(ConsumeContext<Query.GetCartPaymentMethods> context)
    {
        var paymentMethods = await _repository.GetAllAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            predicate: item => item.CartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);

        await context.RespondAsync(paymentMethods switch
        {
            {Page.Size: > 0} => paymentMethods,
            {Page.Size: < 1} => new Reply.NoContent(),
            _ => new Reply.NotFound()
        });
    }
}