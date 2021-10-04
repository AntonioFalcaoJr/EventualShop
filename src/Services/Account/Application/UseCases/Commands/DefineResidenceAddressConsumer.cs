using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using DefineResidenceAddressCommand = Messages.Accounts.Commands.DefineResidenceAddress;

namespace Application.UseCases.Commands
{
    public class DefineResidenceAddressConsumer : IConsumer<DefineResidenceAddressCommand>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public DefineResidenceAddressConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<DefineResidenceAddressCommand> context)
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