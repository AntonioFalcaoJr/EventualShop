using Application.Abstractions.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetInventoryItemDetailsConsumer : IConsumer<Queries.GetInventoryItemDetails>
{
    private readonly IProjectionsRepository<Projections.InventoryProjection> _projectionsRepository;

    public GetInventoryItemDetailsConsumer(IProjectionsRepository<Projections.InventoryProjection> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetInventoryItemDetails> context)
    {
        var inventory = await _projectionsRepository.GetAsync(context.Message.ProductId, context.CancellationToken);
        await context.RespondAsync(inventory);
    }
}