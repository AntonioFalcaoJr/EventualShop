using Contracts.Services.Identity;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Identities;

public static class IdentityApi
{
    public static RouteGroupBuilder MapIdentityApi(this RouteGroupBuilder group)
    {
        group.MapGet("/sign-in", ([AsParameters] Requests.SignIn request)
            => ApplicationApi.QueryAsync(request, (client, cancellationToken) => client.LoginAsync(request, cancellationToken: cancellationToken)));

        group.MapPost("/sign-up", ([AsParameters] Requests.SignUp request)
            => ApplicationApi.SendCommandAsync<Command.RegisterUser>(request));

        group.MapPost("/{userId:guid}/confirm-email", ([AsParameters] Requests.ConfirmEmail request)
            => ApplicationApi.SendCommandAsync<Command.ConfirmEmail>(request));

        group.MapPut("/{userId:guid}/change-email", ([AsParameters] Requests.ChangeEmail request)
            => ApplicationApi.SendCommandAsync<Command.ChangeEmail>(request));

        group.MapPut("/{userId:guid}/change-password", ([AsParameters] Requests.ChangePassword request)
            => ApplicationApi.SendCommandAsync<Command.ChangePassword>(request));

        return group.WithMetadata(new TagsAttribute("Identities"));
    }
}