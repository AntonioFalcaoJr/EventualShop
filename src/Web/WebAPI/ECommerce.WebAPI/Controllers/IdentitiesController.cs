using System.Threading;
using System.Threading.Tasks;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Messages.Services.Identities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
public class IdentitiesController : ApplicationController
{
    public IdentitiesController(IBus bus)
        : base(bus) { }

    [HttpGet]
    public Task<IActionResult> GetUserAuthenticationDetails([FromQuery] Queries.GetUserAuthenticationDetails query, CancellationToken cancellationToken)
        => GetQueryResponseAsync<Queries.GetUserAuthenticationDetails, Responses.UserAuthenticationDetails>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> RegisterUser(Commands.RegisterUser command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> ChangeUserPassword(Commands.ChangeUserPassword command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpDelete]
    public Task<IActionResult> DeleteUser(Commands.DeleteUser command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}