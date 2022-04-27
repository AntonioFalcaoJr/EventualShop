using Application.Abstractions.Projections;
using Contracts.Services.Orders;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectOrderDetailsWhenOrderChangedConsumer : IConsumer<DomainEvent.OrderPlaced>
{
    private readonly IProjectionRepository<Projection.Order> _repository;

    public ProjectOrderDetailsWhenOrderChangedConsumer(IProjectionRepository<Projection.Order> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.OrderPlaced> context)
    {
        var order = new Projection.Order
        {
            Id = context.Message.OrderId,
            IsDeleted = false
        };

        await _repository.InsertAsync(order, context.CancellationToken);
    }
}