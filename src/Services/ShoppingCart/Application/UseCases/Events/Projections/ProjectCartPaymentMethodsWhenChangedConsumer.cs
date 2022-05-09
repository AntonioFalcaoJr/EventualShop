using Application.Abstractions.Projections;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectCartPaymentMethodsWhenChangedConsumer :
    IConsumer<DomainEvent.PaymentMethodAdded>,
    IConsumer<DomainEvent.CartDiscarded>
{
    private readonly IProjectionRepository<Projection.PaymentMethod> _repository;

    public ProjectCartPaymentMethodsWhenChangedConsumer(IProjectionRepository<Projection.PaymentMethod> repository)
    {
        _repository = repository;
    }

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => _repository.DeleteAsync(
            filter: item => item.CartId == context.Message.CartId,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.PaymentMethodAdded> context)
    {
        Projection.PaymentMethod creditCard = new(
            context.Message.MethodId,
            context.Message.CartId,
            context.Message.Amount,
            context.Message.Option,
            false);

        await _repository.InsertAsync(creditCard, context.CancellationToken);
    }
}