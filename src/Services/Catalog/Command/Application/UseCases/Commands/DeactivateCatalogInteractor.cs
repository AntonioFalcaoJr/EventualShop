using Application.Abstractions;
using Application.Services;
using Contracts.Services.Catalog;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public interface IDeactivateCatalogInteractor : IInteractor<Command.DeactivateCatalog> { }
    
public class DeactivateCatalogInteractor : IDeactivateCatalogInteractor
{
    private readonly IApplicationService _applicationService;

    public DeactivateCatalogInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.DeactivateCatalog command, CancellationToken cancellationToken)
    {
        var catalog = await _applicationService.LoadAggregateAsync<Catalog>(command.CatalogId, cancellationToken);
        catalog.Handle(command);
        await _applicationService.AppendEventsAsync(catalog, cancellationToken);
    }
}