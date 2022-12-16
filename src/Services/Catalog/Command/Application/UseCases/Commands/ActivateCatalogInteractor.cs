using Application.Abstractions;
using Application.Services;
using Contracts.Services.Catalog;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public interface IActivateCatalogInteractor : IInteractor<Command.ActivateCatalog> { }

public class ActivateCatalogInteractor : IActivateCatalogInteractor
{
    private readonly IApplicationService _applicationService;

    public ActivateCatalogInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.ActivateCatalog command, CancellationToken cancellationToken)
    {
        var catalog = await _applicationService.LoadAggregateAsync<Catalog>(command.CatalogId, cancellationToken);
        catalog.Handle(command);
        await _applicationService.AppendEventsAsync(catalog, cancellationToken);
    }
}