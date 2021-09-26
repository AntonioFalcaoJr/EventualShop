using System.Threading;
using System.Threading.Tasks;
using ECommerce.WebAPI.Abstractions;
using ECommerce.WebAPI.Messages.Identities;
using MassTransit;
using Messages.Identities.Queries;
using Messages.Identities.Queries.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    public class IdentityController : ApplicationController
    {
        public IdentityController(IBus bus)
            : base(bus) { }

        [HttpGet]
        public Task<IActionResult> GetUserAuthenticationDetails([FromQuery] Queries.GetUserAuthenticationDetailsQuery query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetUserAuthenticationDetails, UserAuthenticationDetails>(query, cancellationToken);

        [HttpPost]
        public Task<IActionResult> RegisterUser(Commands.RegisterUserCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> ChangeUserPassword(Commands.ChangeUserPasswordCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> DeleteUser(Commands.DeleteUserCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}