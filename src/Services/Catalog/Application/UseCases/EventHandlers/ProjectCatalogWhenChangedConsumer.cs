using Application.Abstractions.EventSourcing.Projections;
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
    private readonly IProjectionsRepository<Projections.Catalog> _projectionsRepository;

    public ProjectCatalogWhenChangedConsumer(IProjectionsRepository<Projections.Catalog> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogCreated> context)
    {
        var catalog = new Projections.Catalog(
            context.Message.CatalogId,
            context.Message.Title,
            context.Message.Description,
            context.Message.IsActive,
            context.Message.IsDeleted);

        await _projectionsRepository.InsertAsync(catalog, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvents.CatalogDeleted> context)
        => _projectionsRepository.DeleteAsync(context.Message.CatalogId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogActivated> context)
        => _projectionsRepository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.IsActive,
            value: true,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogDeactivated> context)
        => _projectionsRepository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.IsActive,
            value: false,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogDescriptionChanged> context)
        => await _projectionsRepository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.Description,
            value: context.Message.Description,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogTitleChanged> context)
        => await _projectionsRepository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.Title,
            value: context.Message.Title,
            cancellationToken: context.CancellationToken);
}