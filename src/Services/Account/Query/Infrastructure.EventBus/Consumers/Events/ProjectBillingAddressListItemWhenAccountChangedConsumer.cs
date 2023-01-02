using Application.UseCases.Events;
using Contracts.Services.Account;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectBillingAddressListItemWhenAccountChangedConsumer :
    IConsumer<DomainEvent.BillingAddressAdded>,
    IConsumer<DomainEvent.AccountDeleted>
{
    private readonly IProjectBillingAddressListItemWhenAccountChangedInteractor _interactor;

    public ProjectBillingAddressListItemWhenAccountChangedConsumer(IProjectBillingAddressListItemWhenAccountChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.BillingAddressAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.AccountDeleted> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}