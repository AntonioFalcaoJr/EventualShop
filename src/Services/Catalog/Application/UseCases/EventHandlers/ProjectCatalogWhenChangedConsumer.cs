using Application.EventSourcing.Projections;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.EventHandlers;

public class ProjectCatalogWhenChangedConsumer :
    IConsumer<DomainEvents.CatalogCreated>,
    IConsumer<DomainEvents.CatalogActivated>,
    IConsumer<DomainEvents.CatalogDeactivated>,
    IConsumer<DomainEvents.CatalogDescriptionChanged>,
    IConsumer<DomainEvents.CatalogTitleChanged>,
    IConsumer<DomainEvents.CatalogDeleted>
{
    private readonly ICatalogProjectionsService _projectionsService;

    public ProjectCatalogWhenChangedConsumer(ICatalogProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogCreated> context)
    {
        var catalog = new Projections.Catalog(
            context.Message.CatalogId,
            context.Message.Title,
            context.Message.Description,
            context.Message.IsActive,
            context.Message.IsDeleted);

        await _projectionsService.ProjectAsync(catalog, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvents.CatalogDeleted> context)
        => _projectionsService.RemoveCatalogAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogActivated> context)
        => _projectionsService.UpdateFieldAsync<Projections.Catalog, bool, Guid>(
            id: context.Message.CatalogId,
            field: catalog => catalog.IsActive,
            value: true,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogDeactivated> context)
        => _projectionsService.UpdateFieldAsync<Projections.Catalog, bool, Guid>(
            id: context.Message.CatalogId,
            field: catalog => catalog.IsActive,
            value: false,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogDescriptionChanged> context)
        => await _projectionsService.UpdateFieldAsync<Projections.Catalog, string, Guid>(
            id: context.Message.CatalogId,
            field: catalog => catalog.Description,
            value: context.Message.Description,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogTitleChanged> context)
        => await _projectionsService.UpdateFieldAsync<Projections.Catalog, string, Guid>(
            id: context.Message.CatalogId,
            field: catalog => catalog.Title,
            value: context.Message.Title,
            cancellationToken: context.CancellationToken);
}