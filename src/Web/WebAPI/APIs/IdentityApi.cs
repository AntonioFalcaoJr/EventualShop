using Com.Google.Protobuf;
using Contracts.Services.Identity;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.APIs;

public static class IdentityApi
{
    public static void MapIdentityApi(this RouteGroupBuilder group)
    {
        group.MapQuery("/login", ([FromServices] IdentityService.IdentityServiceClient client, string email, string password, CancellationToken ct)
            => client.LoginAsync(new() {Email = email, Password = password}, cancellationToken: ct).ResponseAsync);

        group.MapCommand(builder => builder.MapPost("/", (IBus bus, Command.RegisterUser command, CancellationToken ct)
            => ApplicationApi.SendCommandAsync(bus, command, ct)));

        group.WithMetadata(new TagsAttribute("IdentitiesV2"));
    }
}