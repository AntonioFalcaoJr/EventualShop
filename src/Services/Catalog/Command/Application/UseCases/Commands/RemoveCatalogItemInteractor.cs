using Application.Abstractions;
using Application.Services;
using Contracts.Services.Catalog;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public interface IRemoveCatalogItemInteractor : IInteractor<Command.RemoveCatalogItem> { }

public class RemoveCatalogItemInteractor : IRemoveCatalogItemInteractor
{
    private readonly IApplicationService _applicationService;

    public RemoveCatalogItemInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.RemoveCatalogItem command, CancellationToken cancellationToken)
    {
        var catalog = await _applicationService.LoadAggregateAsync<Catalog>(command.CatalogId, cancellationToken);
        catalog.Handle(command);
        await _applicationService.AppendEventsAsync(catalog, cancellationToken);
    }
}