using Application.Abstractions.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetInventoryItemDetailsConsumer : IConsumer<Query.GetInventoryItemDetails>
{
    private readonly IProjectionRepository<Projection.Inventory> _repository;

    public GetInventoryItemDetailsConsumer(IProjectionRepository<Projection.Inventory> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetInventoryItemDetails> context)
    {
        var inventory = await _repository.GetAsync(context.Message.ProductId, context.CancellationToken);
        await context.RespondAsync(inventory);
    }
}