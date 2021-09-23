using System;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Entities.Owners;
using MassTransit;
using Messages.Accounts.Commands;

namespace Application.UseCases.Commands
{
    public class DefineAccountOwnerConsumer : IConsumer<DefineAccountOwner>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public DefineAccountOwnerConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<DefineAccountOwner> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

            var owner = new Owner(
                Guid.NewGuid(), 
                context.Message.Age,
                context.Message.Email,
                context.Message.LastName,
                context.Message.Name);

            account.DefineOwner(account.Id, owner);

            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}