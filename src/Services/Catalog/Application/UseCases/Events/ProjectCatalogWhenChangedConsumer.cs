using Application.Abstractions.Projections;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.Events;

public class ProjectCatalogWhenChangedConsumer :
    IConsumer<DomainEvent.CatalogCreated>,
    IConsumer<DomainEvent.CatalogActivated>,
    IConsumer<DomainEvent.CatalogDeactivated>,
    IConsumer<DomainEvent.CatalogDescriptionChanged>,
    IConsumer<DomainEvent.CatalogTitleChanged>,
    IConsumer<DomainEvent.CatalogDeleted>
{
    private readonly IProjectionRepository<Projection.Catalog> _repository;

    public ProjectCatalogWhenChangedConsumer(IProjectionRepository<Projection.Catalog> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.CatalogCreated> context)
    {
        var catalog = new Projection.Catalog(
            context.Message.CatalogId,
            context.Message.Title,
            context.Message.Description,
            context.Message.IsActive,
            context.Message.IsDeleted);

        await _repository.InsertAsync(catalog, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.CatalogDeleted> context)
        => await _repository.DeleteAsync(context.Message.CatalogId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CatalogActivated> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.IsActive,
            value: true,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CatalogDeactivated> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.IsActive,
            value: false,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CatalogDescriptionChanged> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.Description,
            value: context.Message.Description,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.CatalogTitleChanged> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.CatalogId,
            field: catalog => catalog.Title,
            value: context.Message.Title,
            cancellationToken: context.CancellationToken);
}