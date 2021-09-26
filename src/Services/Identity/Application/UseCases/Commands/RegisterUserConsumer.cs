using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates.Users;
using MassTransit;
using Messages.Identities.Commands;

namespace Application.UseCases.Commands
{
    public class RegisterUserConsumer : IConsumer<RegisterUser>
    {
        private readonly IUserEventStoreService _eventStoreService;

        public RegisterUserConsumer(IUserEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<RegisterUser> context)
        {
            var user = new User();

            user.Register(
                context.Message.Password,
                context.Message.PasswordConfirmation,
                context.Message.Login);

            await _eventStoreService.AppendEventsToStreamAsync(user, context.CancellationToken);
        }
    }
}