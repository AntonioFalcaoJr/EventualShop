using Application.Services;
using Domain.Aggregates.CatalogItems;
using MediatR;

namespace Application.UseCases.CatalogItems.Commands;

public record RemoveCatalogItem(CatalogItemId ItemId) : IRequest;

public class RemoveCatalogItemInteractor(IApplicationService service) : IRequestHandler<RemoveCatalogItem>
{
    public async Task Handle(RemoveCatalogItem cmd, CancellationToken cancellationToken)
    {
        var item = await service.LoadAggregateAsync<CatalogItem, CatalogItemId>(cmd.ItemId, cancellationToken);
        item.RemoveCatalogItem(cmd.ItemId);
        await service.AppendEventsAsync<CatalogItem, CatalogItemId>(item, cancellationToken);
    }
}