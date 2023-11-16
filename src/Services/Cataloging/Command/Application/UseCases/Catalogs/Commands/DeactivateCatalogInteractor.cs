using Application.Services;
using Domain.Aggregates.Catalogs;
using MediatR;

namespace Application.UseCases.Catalogs.Commands;

public record DeactivateCatalog(CatalogId CatalogId) : IRequest;

public class DeactivateCatalogInteractor(IApplicationService service) : IRequestHandler<DeactivateCatalog>
{
    public async Task Handle(DeactivateCatalog cmd, CancellationToken cancellationToken)
    {
        var catalog = await service.LoadAggregateAsync<Catalog, CatalogId>(cmd.CatalogId, cancellationToken);
        catalog.Deactivate();
        await service.AppendEventsAsync<Catalog, CatalogId>(catalog, cancellationToken);
    }
}