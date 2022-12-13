using Application.Abstractions;
using Application.Services;
using Contracts.Services.Catalog;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class DeleteCatalogInteractor : IInteractor<Command.DeleteCatalog>
{
    private readonly IApplicationService _applicationService;

    public DeleteCatalogInteractor(IApplicationService applicationService)
        =>  _applicationService = applicationService;

    public async Task InteractAsync(Command.DeleteCatalog command, CancellationToken cancellationToken)
    {
        var catalog = await _applicationService.LoadAggregateAsync<Catalog>(command.CatalogId, cancellationToken);
        catalog.Handle(command);
        await _applicationService.AppendEventsAsync(catalog, cancellationToken);
    }
}