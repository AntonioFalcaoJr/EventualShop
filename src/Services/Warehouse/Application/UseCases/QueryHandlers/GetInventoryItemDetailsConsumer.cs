using Application.EventSourcing.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetInventoryItemDetailsConsumer : IConsumer<Queries.GetInventoryItemDetails>
{
    private readonly IWarehouseProjectionsService _projectionsService;

    public GetInventoryItemDetailsConsumer(IWarehouseProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<Queries.GetInventoryItemDetails> context)
    {
        var inventoryItemDetails = await _projectionsService.GetProductDetailsAsync(context.Message.ProductId, context.CancellationToken);
        await context.RespondAsync<Responses.InventoryItemDetails>(inventoryItemDetails);
    }
}