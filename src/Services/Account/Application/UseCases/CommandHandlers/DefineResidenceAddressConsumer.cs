using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Accounts;

namespace Application.UseCases.CommandHandlers
{
    public class DefineResidenceAddressConsumer : IConsumer<Commands.DefineResidenceAddress>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public DefineResidenceAddressConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.DefineResidenceAddress> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

            account.DefineResidenceAddress(
                account.Id,
                context.Message.City,
                context.Message.Country,
                context.Message.Number,
                context.Message.State,
                context.Message.Street,
                context.Message.ZipCode);

            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}