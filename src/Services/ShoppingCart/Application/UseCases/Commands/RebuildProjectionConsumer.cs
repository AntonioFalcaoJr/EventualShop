using Application.EventStore;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Commands;

public class RebuildProjectionConsumer : IConsumer<Command.RebuildProjection>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public RebuildProjectionConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.RebuildProjection> context)
    {
        await foreach (var aggregate in _eventStore.LoadAggregatesAsync(context.CancellationToken))
        {
            var exchange = "exchange:shopping-cart" +
                           $".{KebabCaseEndpointNameFormatter.Instance.SanitizeName(context.Message.Name)}" +
                           $".{KebabCaseEndpointNameFormatter.Instance.SanitizeName(nameof(IntegrationEvent.ProjectionRebuilt))}";

            var endpoint = await context.GetSendEndpoint(new(exchange));

            await endpoint.Send(
                new IntegrationEvent.ProjectionRebuilt(
                    new(aggregate.Id,
                        aggregate.CustomerId,
                        aggregate.Status,
                        default,
                        default,
                        aggregate.Total,
                        aggregate.TotalPayment,
                        aggregate.AmountDue,
                        aggregate.Items.Select(item => (Dto.CartItem)item),
                        aggregate.PaymentMethods.Select(method => (Dto.PaymentMethod)method))),
                context.CancellationToken);
        }
    }
}