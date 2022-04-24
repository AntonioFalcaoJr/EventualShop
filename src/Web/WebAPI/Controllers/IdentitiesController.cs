using ECommerce.Contracts.Identities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class IdentitiesController : ApplicationController
{
    public IdentitiesController(IBus bus)
        : base(bus) { }

    [HttpGet]
    public Task<IActionResult> GetUserAuthenticationDetails([FromQuery] Query.GetUserAuthentication query, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetUserAuthentication, Projection.UserAuthentication>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> RegisterUser(Command.RegisterUser command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> ChangeUserPassword(Command.ChangeUserPassword command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpDelete]
    public Task<IActionResult> DeleteUser(Command.DeleteUser command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}