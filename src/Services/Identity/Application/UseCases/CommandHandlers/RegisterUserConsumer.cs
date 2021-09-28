using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using Messages.Identities;

namespace Application.UseCases.CommandHandlers
{
    public class RegisterUserConsumer : IConsumer<Commands.RegisterUser>
    {
        private readonly IUserEventStoreService _eventStoreService;

        public RegisterUserConsumer(IUserEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.RegisterUser> context)
        {
            var user = new User();

            user.Register(
                context.Message.Email,
                context.Message.FirstName,
                context.Message.Password,
                context.Message.PasswordConfirmation);

            await _eventStoreService.AppendEventsToStreamAsync(user, context.CancellationToken);
        }
    }
}