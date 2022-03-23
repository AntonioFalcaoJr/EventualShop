using Application.EventSourcing.Projections;
using ECommerce.Contracts.Warehouse;
using MassTransit;
using GetInventoryItemDetailsQuery = ECommerce.Contracts.Warehouse.Queries.GetInventoryItemDetails;

namespace Application.UseCases.Queries;

public class GetInventoryItemDetailsConsumer : IConsumer<GetInventoryItemDetailsQuery>
{
    private readonly IWarehouseProjectionsService _projectionsService;

    public GetInventoryItemDetailsConsumer(IWarehouseProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetInventoryItemDetailsQuery> context)
    {
        var inventoryItemDetails = await _projectionsService.GetProductDetailsAsync(context.Message.ProductId, context.CancellationToken);
        await context.RespondAsync<Responses.InventoryItemDetails>(inventoryItemDetails);
    }
}