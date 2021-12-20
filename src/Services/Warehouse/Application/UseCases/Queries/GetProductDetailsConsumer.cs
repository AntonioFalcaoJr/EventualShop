using Application.EventSourcing.Projections;
using ECommerce.Contracts.Warehouse;
using MassTransit;
using GetProductDetailsQuery = ECommerce.Contracts.Warehouse.Queries.GetProductDetails;

namespace Application.UseCases.Queries;

public class GetProductDetailsConsumer : IConsumer<GetProductDetailsQuery>
{
    private readonly IWarehouseProjectionsService _projectionsService;

    public GetProductDetailsConsumer(IWarehouseProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetProductDetailsQuery> context)
    {
        var productDetails = await _projectionsService.GetProductDetailsAsync(context.Message.ProductId, context.CancellationToken);
        await context.RespondAsync<Responses.ProductDetails>(productDetails);
    }
}