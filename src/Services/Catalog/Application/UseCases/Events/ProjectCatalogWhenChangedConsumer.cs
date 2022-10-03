using Application.Abstractions.Projections;
using Contracts.Services.Catalog;
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
        => _repository = repository;

    public Task Consume(ConsumeContext<DomainEvent.CatalogCreated> context)
    {
        Projection.Catalog catalog = new(
            context.Message.Id,
            context.Message.Title,
            context.Message.Description,
            false,
            false);

        return _repository.InsertAsync(catalog, context.CancellationToken);
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeleted> context)
        => _repository.DeleteAsync(context.Message.Id, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogActivated> context)
        => _repository.UpdateFieldAsync(
            id: context.Message.Id,
            field: catalog => catalog.IsActive,
            value: true,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeactivated> context)
        => _repository.UpdateFieldAsync(
            id: context.Message.Id,
            field: catalog => catalog.IsActive,
            value: false,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogDescriptionChanged> context)
        => _repository.UpdateFieldAsync(
            id: context.Message.Id,
            field: catalog => catalog.Description,
            value: context.Message.Description,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogTitleChanged> context)
        => _repository.UpdateFieldAsync(
            id: context.Message.Id,
            field: catalog => catalog.Title,
            value: context.Message.Title,
            cancellationToken: context.CancellationToken);
}