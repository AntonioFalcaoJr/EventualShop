using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface ICreateCatalogWhenCreatedInteractor : IInteractor<DomainEvent.CatalogCreated> { }

public class CreateCatalogWhenCreatedInteractor : ICreateCatalogWhenCreatedInteractor
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public CreateCatalogWhenCreatedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogCreated @event, CancellationToken cancellationToken)
    {
        Projection.Catalog catalog = new(
            @event.CatalogId,
            @event.Title,
            @event.Description,
            default,
            default);

        await _projectionGateway.InsertAsync(catalog, cancellationToken);
    }
}