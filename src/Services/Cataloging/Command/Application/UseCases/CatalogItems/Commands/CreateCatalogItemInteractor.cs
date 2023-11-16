using Application.Services;
using Domain.Aggregates;
using Domain.Aggregates.CatalogItems;
using Domain.Aggregates.Catalogs;
using Domain.Aggregates.Products;
using MediatR;

namespace Application.UseCases.CatalogItems.Commands;

public record CreateCatalogItem(AppId AppId, CatalogId CatalogId, ProductId ProductId) : IRequest;

public class CreateCatalogItemInteractor(IApplicationService service) : IRequestHandler<CreateCatalogItem>
{
    public Task Handle(CreateCatalogItem cmd, CancellationToken cancellationToken)
    {
        var newItem = CatalogItem.Create(cmd.AppId, cmd.CatalogId, cmd.ProductId);
        return service.AppendEventsAsync<CatalogItem, CatalogItemId>(newItem, cancellationToken);
    }
}