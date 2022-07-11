using Application.Abstractions.Projections;
using Contracts.Services.Account;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectAddressWhenChangedConsumer :
    IConsumer<DomainEvent.ShippingAddressAdded>,
    IConsumer<DomainEvent.BillingAddressAdded>
{
    private readonly IProjectionRepository<Projection.Address> _repository;

    public ProjectAddressWhenChangedConsumer(IProjectionRepository<Projection.Address> repository)
        => _repository = repository;

    public async Task Consume(ConsumeContext<DomainEvent.BillingAddressAdded> context)
    {
        Projection.BillingAddress address = new(context.Message.AddressId, context.Message.AccountId, context.Message.Address, false);
        await _repository.InsertAsync(address, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.ShippingAddressAdded> context)
    {
        Projection.ShippingAddress address = new(context.Message.AddressId, context.Message.AccountId, context.Message.Address, false);
        await _repository.InsertAsync(address, context.CancellationToken);
    }
}