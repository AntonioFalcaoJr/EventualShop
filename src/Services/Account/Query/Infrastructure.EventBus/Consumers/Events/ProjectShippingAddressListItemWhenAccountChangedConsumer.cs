using Application.UseCases.Events;
using Contracts.Services.Account;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectShippingAddressListItemWhenAccountChangedConsumer :
    IConsumer<DomainEvent.ShippingAddressAdded>,
    IConsumer<DomainEvent.AccountDeleted>
{
    private readonly IProjectShippingAddressListItemWhenAccountChangedInteractor _interactor;

    public ProjectShippingAddressListItemWhenAccountChangedConsumer(IProjectShippingAddressListItemWhenAccountChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.ShippingAddressAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountDeleted> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}