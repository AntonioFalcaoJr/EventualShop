using Application.Abstractions.Projections;
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
    private readonly IProjectionRepository<Projection.Catalog> _projectionRepository;

    public ProjectCatalogWhenChangedConsumer(IProjectionRepository<Projection.Catalog> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogCreated> context)
    {
        var catalog = new Projection.Catalog(
            context.Message.CatalogId,
            context.Message.Title,
            context.Message.Description,
            context.Message.IsActive,
            context.Message.IsDeleted);

        await _projectionRepository.InsertAsync(catalog, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogDeleted> context)
        => await _projectionRepository.DeleteAsync(context.Message.CatalogId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogActivated> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.IsActive,
            value: true,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogDeactivated> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.IsActive,
            value: false,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogDescriptionChanged> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.Description,
            value: context.Message.Description,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogTitleChanged> context)
        => await _projectionRepository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.Title,
            value: context.Message.Title,
            cancellationToken: context.CancellationToken);
}