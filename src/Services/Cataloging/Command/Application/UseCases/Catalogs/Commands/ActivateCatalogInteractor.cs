using Application.Services;
using Domain.Aggregates.Catalogs;
using MediatR;

namespace Application.UseCases.Catalogs.Commands;

public record ActivateCatalog(CatalogId CatalogId) : IRequest;

public class ActivateCatalogInteractor(IApplicationService service) : IRequestHandler<ActivateCatalog>
{
    public async Task Handle(ActivateCatalog cmd, CancellationToken cancellationToken)
    {
        var catalog = await service.LoadAggregateAsync<Catalog, CatalogId>(cmd.CatalogId, cancellationToken);
        catalog.Activate();
        await service.AppendEventsAsync<Catalog, CatalogId>(catalog, cancellationToken);
    }
}