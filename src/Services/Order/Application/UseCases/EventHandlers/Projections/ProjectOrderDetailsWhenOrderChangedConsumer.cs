using Application.Abstractions.Projections;
using ECommerce.Contracts.Orders;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectOrderDetailsWhenOrderChangedConsumer : IConsumer<DomainEvents.OrderPlaced>
{
    private readonly IProjectionRepository<ECommerce.Contracts.Orders.Projection.Order> _repository;

    public ProjectOrderDetailsWhenOrderChangedConsumer(IProjectionRepository<ECommerce.Contracts.Orders.Projection.Order> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.OrderPlaced> context)
    {
        var order = new ECommerce.Contracts.Orders.Projection.Order
        {
            Id = context.Message.OrderId,
            IsDeleted = false
        };

        await _repository.InsertAsync(order, context.CancellationToken);
    }
}