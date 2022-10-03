using Application.Abstractions.Projections;
using Contracts.Services.Order;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectOrderDetailsWhenOrderChangedConsumer : IConsumer<DomainEvent.OrderPlaced>
{
    private readonly IProjectionRepository<Projection.Order> _repository;

    public ProjectOrderDetailsWhenOrderChangedConsumer(IProjectionRepository<Projection.Order> repository)
        => _repository = repository;

    public async Task Consume(ConsumeContext<DomainEvent.OrderPlaced> context)
    {
        Projection.Order order = new(context.Message.Id, false);
        await _repository.InsertAsync(order, context.CancellationToken);
    }
}