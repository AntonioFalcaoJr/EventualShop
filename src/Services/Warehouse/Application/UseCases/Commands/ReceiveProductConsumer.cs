using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using ReceiveProductCommand = ECommerce.Contracts.Warehouse.Commands.ReceiveProduct;

namespace Application.UseCases.Commands;

public class ReceiveProductConsumer : IConsumer<ReceiveProductCommand>
{
    private readonly IWarehouseEventStoreService _eventStoreService;

    public ReceiveProductConsumer(IWarehouseEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<ReceiveProductCommand> context)
    {
        var product = new Product();
        product.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(product, context.CancellationToken);
    }
}